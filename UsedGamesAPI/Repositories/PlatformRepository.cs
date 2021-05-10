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
    public class PlatformRepository : IPlatformRepository
    {
        private readonly DataContext _dataContext;

        public PlatformRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Platform> FindByIdAsync(int id) => await _dataContext.Platform.FindAsync(id);
        public async Task<Platform> FindByIdWithGamesAsync(int id) => await _dataContext.Platform.Include(p => p.Games).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<PagedList<Platform>> FindAllAsync() => (await _dataContext.Platform.ToListAsync()).ToPagedList();

        public async Task<List<Platform>> FindAllWithGamesAsync() => await _dataContext.Platform.Include(p => p.Games).ToListAsync();

        public async Task CreateAsync(Platform obj)
        {
            _dataContext.Platform.Add(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Platform obj)
        {
            _dataContext.Platform.Remove(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Platform obj)
        {
            _dataContext.Platform.Update(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id) => await _dataContext.Platform.FindAsync(id) != null;
    }
}
