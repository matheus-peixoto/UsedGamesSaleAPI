using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.Data;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repository.Interfaces;

namespace UsedGamesAPI.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _dataContext;

        public ClientRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Client> FindByIdAsync(int id) => await _dataContext.Client.FindAsync(id);

        public async Task<List<Client>> FindAllAsync() => await _dataContext.Client.ToListAsync();

        public async Task<List<Client>> FindAllWithOrderAsync() => await _dataContext.Client.Include(s => s.Orders).ToListAsync();

        public async Task CreateAsync(Client obj)
        {
            _dataContext.Client.Add(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Client obj)
        {
            _dataContext.Client.Remove(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client obj)
        {
            _dataContext.Client.Update(obj);
            await _dataContext.SaveChangesAsync();
        }
    }
}
