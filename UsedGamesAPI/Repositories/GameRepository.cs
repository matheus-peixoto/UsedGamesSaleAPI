using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.Data;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories.Interfaces;

namespace UsedGamesAPI.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly DataContext _dataContext;

        public GameRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Game> FindByIdAsync(int id) => await _dataContext.Game.Include(g => g.Platform).Include(g => g.Images).FirstOrDefaultAsync(g => g.Id == id);

        public async Task<List<Game>> FindAllAsync() => await _dataContext.Game.Include(g => g.Platform).ToListAsync();

        public async Task<List<Game>> FindAllBySellerAsync(int sellerId) => await _dataContext.Game.Include(g => g.Platform).Where(g => g.SellerId == sellerId).ToListAsync();

        public async Task<Image> FindGameImageAsync(int gameId, int imgId)
        {
            Game game = await _dataContext.Game.Include(g => g.Images).FirstOrDefaultAsync(g => g.Id == gameId);
            if (game is null) return null;
            Image img = game.Images.FirstOrDefault(i => i.Id == imgId);
            return img;
        }

        public async Task CreateAsync(Game obj)
        {
            _dataContext.Game.Add(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Game obj)
        {
            _dataContext.Game.Remove(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Game obj)
        {
            _dataContext.Game.Update(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id) => await _dataContext.Game.FindAsync(id) != null;
    }
}
