using UsedGamesAPI.Models;

namespace UsedGamesAPI.Repositories.Interfaces
{
    public interface IClientRepository: ICrud<Client>, IUserRepository<Client>
    {
    }
}
