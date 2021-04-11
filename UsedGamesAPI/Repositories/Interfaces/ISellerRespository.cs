using UsedGamesAPI.Models;

namespace UsedGamesAPI.Repositories.Interfaces
{
    public interface ISellerRespository: ICrud<Seller>, IUserRepository<Seller>
    {
    }
}
