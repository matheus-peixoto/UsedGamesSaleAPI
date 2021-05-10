using System.Collections.Generic;
using System.Threading.Tasks;
using UsedGamesAPI.Models;
using UsedGamesAPI.Services.Paging;

namespace UsedGamesAPI.Repositories.Interfaces
{
    public interface ICrud<T>
    {
        public Task<T> FindByIdAsync(int id);
        public Task<PagedList<T>> FindAllAsync();
        public Task CreateAsync(T obj);
        public Task UpdateAsync(T obj);
        public Task DeleteAsync(T obj);
        public Task<bool> ExistsAsync(int id);
    }
}
