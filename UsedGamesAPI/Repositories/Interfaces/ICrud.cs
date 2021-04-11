using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsedGamesAPI.Repository.Interfaces
{
    public interface ICrud<T>
    {
        public Task<T> FindByIdAsync(int id);
        public Task<List<T>> FindAllAsync();
        public Task CreateAsync(T obj);
        public Task UpdateAsync(T obj);
        public Task DeleteAsync(T obj);
    }
}
