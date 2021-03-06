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
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _dataContext;

        public OrderRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Order> FindByIdAsync(int id) => await _dataContext.Order.Include(o => o.Client).FirstOrDefaultAsync(o => o.Id == id);

        public async Task<PagedList<Order>> FindAllAsync() => (await _dataContext.Order.Include(o => o.Client).ToListAsync()).ToPagedList();

        public async Task CreateAsync(Order obj)
        {
            _dataContext.Order.Add(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order obj)
        {
            _dataContext.Order.Remove(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order obj)
        {
            _dataContext.Order.Update(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id) => await _dataContext.Order.FindAsync(id) != null;
    }
}
