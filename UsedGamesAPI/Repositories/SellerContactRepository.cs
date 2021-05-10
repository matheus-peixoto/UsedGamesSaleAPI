using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsedGamesAPI.Data;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories.Interfaces;
using UsedGamesAPI.Services.ExthensionsMethods;
using UsedGamesAPI.Services.Paging;

namespace UsedGamesAPI.Repositories
{
    public class SellerContactRepository : ISellerContactRepository
    {
        private readonly DataContext _dataContext;

        public SellerContactRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<SellerContact> FindByIdAsync(int id) => await _dataContext.SellerContact.FindAsync(id);

        public async Task<PagedList<SellerContact>> FindAllAsync() => (await _dataContext.SellerContact.ToListAsync()).ToPagedList();

        public async Task CreateAsync(SellerContact obj)
        {
            _dataContext.SellerContact.Add(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(SellerContact obj)
        {
            _dataContext.SellerContact.Remove(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(SellerContact obj)
        {
            _dataContext.SellerContact.Update(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id) => await _dataContext.SellerContact.FindAsync(id) != null;
    }
}
