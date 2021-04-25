using System.Threading.Tasks;

namespace UsedGamesAPI.Repositories.Interfaces
{
    public interface IUserRepository<T>
    {
        public Task<T> FindByAccountAsync(string email, string password);
    }
}
