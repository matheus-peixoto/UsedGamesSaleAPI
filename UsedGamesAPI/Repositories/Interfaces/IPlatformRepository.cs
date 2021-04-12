using System.Collections.Generic;
using System.Threading.Tasks;
using UsedGamesAPI.Models;

namespace UsedGamesAPI.Repositories.Interfaces
{
    public interface IPlatformRepository : ICrud<Platform>
    {
        public Task<List<Platform>> FindAllWithGamesAsync();
    }
}
