using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.GrandComicDatabase
{
    public class GrandComicDatabaseClient : IGrandComicDatabaseClient
    {
        private readonly HttpClient _httpClient;
        private readonly RequestSender _requestSender;

        public GrandComicDatabaseClient(HttpClient httpClient, RequestSender requestSender)
        {
            _httpClient = httpClient;
            _requestSender = requestSender;
        }

        public async Task<List<ClientDTO>> GetClients(CancellationToken cancellationToken)
        {
            string uri = "clients";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);

            List<ClientDTO> clients = await _requestSender.SendRequest<List<ClientDTO>>(request, _httpClient, cancellationToken);

            return clients;
        }
    }
}
