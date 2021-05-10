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
    public class ManagerRepository : IManagerRepository
    {
        private readonly DataContext _dataContext;

        public ManagerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Manager> FindByIdAsync(int id) => await _dataContext.Manager.FirstOrDefaultAsync(m => m.Id == id);

        public async Task<Manager> FindByAccountAsync(string email, string password) => await _dataContext.Manager.FirstOrDefaultAsync(m => m.Email == email && m.Password == password);

        public async Task<PagedList<Manager>> FindAllAsync() => (await _dataContext.Manager.ToListAsync()).ToPagedList();

        public async Task CreateAsync(Manager obj)
        {
            _dataContext.Manager.Add(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Manager obj)
        {
            _dataContext.Manager.Remove(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Manager obj)
        {
            _dataContext.Manager.Update(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id) => await _dataContext.Manager.FindAsync(id) != null;
    }
}
