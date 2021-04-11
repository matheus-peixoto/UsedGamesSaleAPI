using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsedGamesAPI.Data;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories.Interfaces;

namespace UsedGamesAPI.Repositories
{
    public class SellerRepository : ISellerRespository
    {
        private readonly DataContext _dataContext;

        public SellerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Seller> FindByIdAsync(int id) => await _dataContext.Seller.Include(s => s.Contact).FirstOrDefaultAsync(s => s.Id == id);

        public async Task<Seller> FindByIdWithOrderAsync(int id) 
            => await _dataContext.Seller.Include(s => s.Contact).Include(s => s.Orders).FirstOrDefaultAsync(s => s.Id == id);

        public async Task<List<Seller>> FindAllAsync() => await _dataContext.Seller.Include(s => s.Contact).ToListAsync();

        public async Task<List<Seller>> FindAllWithOrderAsync() => await _dataContext.Seller.Include(s => s.Contact).Include(s => s.Orders).ToListAsync();

        public async Task CreateAsync(Seller obj)
        {
            _dataContext.Seller.Add(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Seller obj)
        {
            _dataContext.Seller.Remove(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj)
        {
            _dataContext.Seller.Update(obj);
            await _dataContext.SaveChangesAsync();
        }
    }
}
