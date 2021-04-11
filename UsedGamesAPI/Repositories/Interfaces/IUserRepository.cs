using System.Collections.Generic;
using System.Threading.Tasks;

namespace UsedGamesAPI.Repositories.Interfaces
{
    public interface IUserRepository<T>
    {
        public Task<T> FindByIdWithOrderAsync(int id);
        public Task<List<T>> FindAllWithOrderAsync();
    }
}
