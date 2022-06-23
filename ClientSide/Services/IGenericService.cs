using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TicketSystem.Services
{
    public interface IGenericService<T> where T : class
    {
        Task<IEnumerable<T>> GetEntities();
        Task<HttpResponseMessage> CreateEntity(T newEntity);
        Task<T> GetEntity(Guid entityId);
        Task<HttpResponseMessage> UpdateEntity(T upEntity);
        Task<HttpResponseMessage> DeleteEntity(Guid ticketId);

    }
}
