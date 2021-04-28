using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.Models;

namespace UsedGamesAPI.Repositories.Interfaces
{
    public interface IImageRepository : ICrud<Image>
    {
        public Task<List<Image>> FindAllByGameAsync(int gameId);
    }
}
