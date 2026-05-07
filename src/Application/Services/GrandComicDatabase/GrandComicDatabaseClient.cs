using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace Application.Services.GrandComicDatabase
{
    public class GrandComicDatabaseClient : IGrandComicDatabaseClient
    {
        private readonly HttpClient _httpClient;
        private readonly RequestSender _requestSender;

        public GrandComicDatabaseClient(HttpClient httpClient, RequestSender requestSender)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("CalliopeComicsServer/1.0");
            _requestSender = requestSender;
        }

        public async Task<Issue> GetIssue(int id, CancellationToken cancellationToken)
        {
            string uri = $"issue/{id}/?format=json";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);

            Issue issue = await _requestSender.SendRequest<Issue>(request, _httpClient, cancellationToken);

            return issue;
        }

        public async Task<byte[]> GetIssueCover(Issue issue, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(issue.Cover))
            {
                return new byte[0];
            }

            try
            {
                using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, issue.Cover);
                request.Headers.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/125.0.0.0 Safari/537.36");
                request.Headers.Accept.ParseAdd("image/avif,image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8");
                request.Headers.AcceptLanguage.ParseAdd("en-US,en;q=0.9");
                request.Headers.Referrer = new Uri("https://www.comics.org/");
                request.Headers.AcceptEncoding.ParseAdd("gzip, deflate");

                using HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsByteArrayAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return new byte[0];
            }
        }
    }
}
