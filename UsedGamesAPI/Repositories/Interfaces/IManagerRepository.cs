using UsedGamesAPI.Models;

namespace UsedGamesAPI.Repositories.Interfaces
{
    public interface IManagerRepository : ICrud<Manager>, IUserRepository<Manager>
    {
    }
}
