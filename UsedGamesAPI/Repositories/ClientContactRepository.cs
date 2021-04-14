using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsedGamesAPI.Data;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories.Interfaces;

namespace UsedGamesAPI.Repositories
{
    public class ClientContactRepository : IClientContactRepository
    {
        private readonly DataContext _dataContext;

        public ClientContactRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ClientContact> FindByIdAsync(int id) => await _dataContext.ClientContact.FindAsync(id);

        public async Task<List<ClientContact>> FindAllAsync() => await _dataContext.ClientContact.ToListAsync();

        public async Task CreateAsync(ClientContact obj)
        {
            _dataContext.ClientContact.Add(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClientContact obj)
        {
            _dataContext.ClientContact.Remove(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClientContact obj)
        {
            _dataContext.ClientContact.Update(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id) => await _dataContext.ClientContact.FindAsync(id) != null;
    }
}
