using System.Collections.Generic;
using System.Threading.Tasks;

namespace UsedGamesAPI.Repositories.Interfaces
{
    public interface IUserRepository<T>
    {
        public Task<T> FindByIdWithOrdersAsync(int id);
        public Task<List<T>> FindAllWithOrdersAsync();
    }
}
