using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TicketSystem.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly HttpClient httpClient;
        [Inject]
        public ISessionStorageService sessionStorage { get; set; }
        public string token { get; set; }
        
        public Type myType = typeof(T);

        public GenericService(HttpClient httpClient, ISessionStorageService session )
        {
            this.httpClient = httpClient;
            sessionStorage = session;        

        }

        public async Task GetToken()
        {
            token = await sessionStorage.GetItemAsync<string>("token");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<HttpResponseMessage> CreateEntity(T newEntity)
        {
            await GetToken();
            
            var response = await httpClient.PostAsJsonAsync<T>($"api/{myType.Name}", newEntity);
            return response;

        }
        public async Task<IEnumerable<T>> GetEntities()
        {
            await GetToken();
            return await httpClient.GetFromJsonAsync<T[]>($"api/{myType.Name}");
        }
            
        public async Task<T> GetEntity(Guid entityId)
        {

            await GetToken();
            var response = await httpClient.GetFromJsonAsync<T>($"api/{myType.Name}/{entityId}");
            return response;
        }

        public async Task<HttpResponseMessage> UpdateEntity(T upEntity)
        {
            await GetToken();
            var response = await httpClient.PutAsJsonAsync<T>($"api/{myType.Name}/{myType.GetProperty("Id").GetValue(upEntity)}", upEntity);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteEntity(Guid ticketId)
        {
            await GetToken();
            var response = await httpClient.DeleteAsync($"api/{myType.Name}/{ticketId}");
            return response;
        }
    }
}
