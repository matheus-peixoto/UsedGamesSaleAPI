using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.Data;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories.Interfaces;

namespace UsedGamesAPI.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly DataContext _dataContext;

        public ImageRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Image> FindByIdAsync(int id) => await _dataContext.Image.FirstOrDefaultAsync(g => g.Id == id);

        public async Task<List<Image>> FindAllAsync() => await _dataContext.Image.ToListAsync();

        public async Task<List<Image>> FindAllByGameAsync(int gameId) => await _dataContext.Image.Where(i => i.GameId == gameId).ToListAsync();

        public async Task CreateAsync(Image obj)
        {
            _dataContext.Image.Add(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Image obj)
        {
            _dataContext.Image.Remove(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Image obj)
        {
            _dataContext.Image.Update(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id) => await _dataContext.Image.FindAsync(id) != null;
    }
}
