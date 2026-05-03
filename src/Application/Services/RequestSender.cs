using Domain.DTO;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime;
using System.Text;
using System.Text.Json;

namespace Application.Services
{
    public class RequestSender
    {
        private readonly SemaphoreSlim _rateLimitSemaphore;
        private readonly int _maxRequests;
        private readonly TimeSpan _timeWindow;
        private DateTime _windowStart;
        private int _requestCount;
        private readonly ApplicationSettings _settings;

        public RequestSender(IOptions<ApplicationSettings> settings)
        {
            _maxRequests = 100; // Maximum 100 requests
            _timeWindow = TimeSpan.FromMilliseconds(1000); // Per 1000 ms
            _rateLimitSemaphore = new SemaphoreSlim(_maxRequests, _maxRequests);
            _windowStart = DateTime.UtcNow;
            _requestCount = 2;
            _settings = settings.Value;
        }

        public async Task<T> SendRequest<T>(HttpRequestMessage request, HttpClient httpClient, CancellationToken cancellationToken)
        {
            await EnforceRateLimitAsync(cancellationToken);

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            cancellationToken.ThrowIfCancellationRequested();

            HttpResponseMessage response = await httpClient.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return await ProcessResponse<T>(response, request.Method);
            }

            HttpStatusCode statusCode = response.StatusCode; // Get the status code

            if (statusCode == (System.Net.HttpStatusCode)429)
            {
                await Task.Delay(_timeWindow);
                HttpRequestMessage clonedRequest = await CloneHttpRequestMessage(request);
                response = await httpClient.SendAsync(clonedRequest, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return await ProcessResponse<T>(response, request.Method);
                }
            }

            string responseText;
            using (StreamReader reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                responseText = reader.ReadToEnd();
            }

            if (statusCode == HttpStatusCode.Unauthorized || statusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException(responseText);
            }
            else
            {
                throw new HttpRequestException(responseText);
            }
        }

        private static async Task<T> ProcessResponse<T>(HttpResponseMessage response, HttpMethod method)
        {
            if (method == HttpMethod.Delete)
            {
                return default;
            }

            if (typeof(T) == typeof(byte[]))
            {
                object bytes = await response.Content.ReadAsByteArrayAsync();
                return (T)bytes;
            }

            // Check if T is a generic list
            if (response.StatusCode == HttpStatusCode.NoContent &&
                typeof(T).IsGenericType &&
                typeof(T).GetGenericTypeDefinition() == typeof(List<>))
            {
                return (T)Activator.CreateInstance(typeof(T)); // Return an empty list of type T
            }

            Stream stream = await response.Content.ReadAsStreamAsync();

            if (!stream.CanRead)
            {
                throw new Exception("Stream cannot be read");
            }

            JsonSerializerOptions options = new JsonSerializerOptions();
            //options.Converters.Add(new DateTimeConverterUsingDateTimeParse());
            return JsonSerializer.Deserialize<T>(stream, options);
        }

        private async Task<HttpRequestMessage> CloneHttpRequestMessage(HttpRequestMessage request)
        {
            HttpRequestMessage clone = new HttpRequestMessage(request.Method, request.RequestUri);

            // Copy headers
            foreach (KeyValuePair<string, IEnumerable<string>> header in request.Headers)
            {
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            // Copy content
            if (request.Content != null)
            {
                clone.Content = new StreamContent(await request.Content.ReadAsStreamAsync());
                foreach (KeyValuePair<string, IEnumerable<string>> header in request.Content.Headers)
                {
                    clone.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            return clone;
        }

        public async Task EnforceRateLimitAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                lock (_rateLimitSemaphore)
                {
                    DateTime now = DateTime.UtcNow;
                    if (now - _windowStart > _timeWindow)
                    {
                        _windowStart = now;
                        _requestCount = 0;
                    }

                    if (_requestCount < _maxRequests)
                    {
                        _requestCount++;
                        break;
                    }
                }

                await Task.Delay(100, cancellationToken); // Wait before retrying
            }

            await _rateLimitSemaphore.WaitAsync(cancellationToken);
            _ = Task.Delay(_timeWindow).ContinueWith(_ => _rateLimitSemaphore.Release());
        }
    }

}
