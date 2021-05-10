using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsedGamesAPI.Data;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories.Interfaces;
using UsedGamesAPI.Services.ExthensionsMethods;
using UsedGamesAPI.Services.Paging;

namespace UsedGamesAPI.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _dataContext;

        public ClientRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Client> FindByIdAsync(int id) => await _dataContext.Client.Include(c => c.ClientContact).FirstOrDefaultAsync(s => s.Id == id);

        public async Task<Client> FindByAccountAsync(string email, string password) => await _dataContext.Client.FirstOrDefaultAsync(c => c.Email == email && c.Password == password);

        public async Task<Client> FindByIdWithOrdersAsync(int id) 
            => await _dataContext.Client.Include(c => c.ClientContact).Include(c => c.Orders).FirstOrDefaultAsync(c => c.Id == id);

        public async Task<PagedList<Client>> FindAllAsync() => (await _dataContext.Client.Include(c => c.ClientContact).ToListAsync()).ToPagedList();

        public async Task<List<Client>> FindAllWithOrdersAsync() => await _dataContext.Client.Include(c => c.ClientContact).Include(s => s.Orders).ToListAsync();

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

        public async Task<bool> ExistsAsync(int id) => await _dataContext.Client.FindAsync(id) != null;
    }
}
