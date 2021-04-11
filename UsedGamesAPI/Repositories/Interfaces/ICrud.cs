using System.Collections.Generic;
using System.Threading.Tasks;

namespace UsedGamesAPI.Repositories.Interfaces
{
    public interface ICrud<T>
    {
        public Task<T> FindByIdAsync(int id);
        public Task<List<T>> FindAllAsync();
        public Task CreateAsync(T obj);
        public Task UpdateAsync(T obj);
        public Task DeleteAsync(T obj);
        public Task<bool> Exists(int id);
    }
}
