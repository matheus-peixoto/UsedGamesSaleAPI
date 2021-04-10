using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.Data;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repository.Interfaces;

namespace UsedGamesAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _dataContext;

        public OrderRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Order> FindByIdAsync(int id) => await _dataContext.Order.FindAsync(id);

        public async Task<List<Order>> FindAllAsync() => await _dataContext.Order.ToListAsync();

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
    }
}
