using System.Collections.Generic;
using System.Threading.Tasks;
using UsedGamesAPI.Models;

namespace UsedGamesAPI.Repositories.Interfaces
{
    public interface IPlatformRepository : ICrud<Platform>
    {
        public Task<Platform> FindByIdWithGamesAsync(int id);
        public Task<List<Platform>> FindAllWithGamesAsync();
    }
}
