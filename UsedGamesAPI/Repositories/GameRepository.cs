using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<Game> FindByIdAsync(int id) => await _dataContext.Game.FindAsync(id);

        public async Task<List<Game>> FindAllAsync() => await _dataContext.Game.ToListAsync();

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
    }
}
