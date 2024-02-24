using DAL.DB_Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Services
{
    public class ClientService : IClientService
    {
        private readonly ShopContext _shopContext;

        public ClientService(ShopContext shopContext) =>  _shopContext = shopContext;

        public async Task<Client> AddClient(Client client)
        {
            _shopContext.Clients.Add(client);
            await _shopContext.SaveChangesAsync();

            return client;
        }

        public async Task<int> DeleteClient(int id)
        {
            var client = await _shopContext.Clients
                                           .FirstOrDefaultAsync(x => x.ClientId == id);

            if (client == null)
            {
                return (int)default;
            }

            _shopContext.Clients.Remove(client);
            return await _shopContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Client>> FiltersClientsByEmail(string email)
        {
            return await _shopContext.Clients
                                     .Where(p => p.Email.Contains(email))
                                     .ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await _shopContext.Clients
                                     .ToListAsync();
        }

        public async Task<Client> GetClientById(int id)
        {
            var client = await _shopContext.Clients
                                           .FirstOrDefaultAsync(x => x.ClientId == id);

            return client;
        }

        public async Task<Client> UpdateClient(Client client)
        {
            _shopContext.Entry(client).State = EntityState.Modified;
            await _shopContext.SaveChangesAsync();

            return client;
        }
    }

    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAllClients();

        Task<Client> GetClientById(int id);

        Task<Client> AddClient(Client client);

        Task<Client> UpdateClient(Client client);

        Task<int> DeleteClient(int id);

        Task<IEnumerable<Client>> FiltersClientsByEmail(string email);
    }
}
