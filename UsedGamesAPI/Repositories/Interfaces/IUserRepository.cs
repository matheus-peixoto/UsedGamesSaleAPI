using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsedGamesAPI.Repository.Interfaces
{
    public interface IUserRepository<T>
    {
        public Task<T> FindByIdWithOrderAsync(int id);
        public Task<List<T>> FindAllWithOrderAsync();
    }
}
