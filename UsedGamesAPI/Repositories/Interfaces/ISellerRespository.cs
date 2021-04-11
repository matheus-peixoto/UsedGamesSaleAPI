using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.Models;

namespace UsedGamesAPI.Repository.Interfaces
{
    public interface ISellerRespository: ICrud<Seller>, IUserRepository<Seller>
    {
    }
}
