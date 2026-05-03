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
                throw new ArgumentException("Issue cover URL is null or empty", nameof(issue));
            }

            return await _httpClient.GetByteArrayAsync(issue.Cover, cancellationToken);
        }
    }
}
