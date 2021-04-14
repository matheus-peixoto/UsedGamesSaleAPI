using System.Collections.Generic;
using System.Threading.Tasks;
using UsedGamesAPI.Models;

namespace UsedGamesAPI.Repositories.Interfaces
{
    public interface IClientRepository: ICrud<Client>
    {
        public Task<Client> FindByIdWithOrdersAsync(int id);
        public Task<List<Client>> FindAllWithOrdersAsync();
    }
}
