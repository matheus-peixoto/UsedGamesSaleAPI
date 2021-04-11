using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsedGamesAPI.Data;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories.Interfaces;

namespace UsedGamesAPI.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _dataContext;

        public ClientRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Client> FindByIdAsync(int id) => await _dataContext.Client.Include(c => c.Contact).FirstOrDefaultAsync(s => s.Id == id);

        public async Task<Client> FindByIdWithOrderAsync(int id) 
            => await _dataContext.Client.Include(c => c.Contact).Include(c => c.Orders).FirstOrDefaultAsync(c => c.Id == id);

        public async Task<List<Client>> FindAllAsync() => await _dataContext.Client.Include(c => c.Contact).ToListAsync();

        public async Task<List<Client>> FindAllWithOrderAsync() => await _dataContext.Client.Include(c => c.Contact).Include(s => s.Orders).ToListAsync();

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

        public async Task<bool> Exists(int id) => await _dataContext.Client.FindAsync(id) != null;
    }
}
