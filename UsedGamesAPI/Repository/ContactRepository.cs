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
    public class ContactRepository : IContactRepository
    {
        private readonly DataContext _dataContext;

        public ContactRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Contact> FindByIdAsync(int id) => await _dataContext.Contact.FindAsync(id);

        public async Task<List<Contact>> FindAllAsync() => await _dataContext.Contact.ToListAsync();

        public async Task CreateAsync(Contact obj)
        {
            _dataContext.Contact.Add(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Contact obj)
        {
            _dataContext.Contact.Remove(obj);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contact obj)
        {
            _dataContext.Contact.Update(obj);
            await _dataContext.SaveChangesAsync();
        }
    }
}
