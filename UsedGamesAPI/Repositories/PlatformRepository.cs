
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsedGamesAPI.Data;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repository.Interfaces;

namespace UsedGamesAPI.Repository
{
    public class PlatformRespository : IPlatformRepository
    {
        private readonly DataContext _dataContext;

        public PlatformRespository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Platform> FindByIdAsync(int id) => await _dataContext.Platform.FindAsync(id);

        public async Task<List<Platform>> FindAllAsync() => await _dataContext.Platform.ToListAsync();

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
    }
}
