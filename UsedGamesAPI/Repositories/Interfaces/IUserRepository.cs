using System.Threading.Tasks;

namespace UsedGamesAPI.Repositories.Interfaces
{
    public interface IUserRepository<T>
    {
        public Task<T> FindByAccount(string email, string password);
    }
}
