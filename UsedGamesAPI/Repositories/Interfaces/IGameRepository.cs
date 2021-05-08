using System.Collections.Generic;
using System.Threading.Tasks;
using UsedGamesAPI.Models;

namespace UsedGamesAPI.Repositories.Interfaces
{
    public interface IGameRepository : ICrud<Game>
    {
        public Task<List<Game>> FindAllBySellerAsync(int sellerId);
        public Task<Image> FindGameImageAsync(int gameId, int imgId);
    }
}
