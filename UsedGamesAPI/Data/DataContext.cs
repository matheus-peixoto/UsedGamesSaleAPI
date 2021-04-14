using Microsoft.EntityFrameworkCore;
using UsedGamesAPI.Models;

namespace UsedGamesAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Client> Client { get; set; }
        public DbSet<ClientContact> ClientContact { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SellerContact> SellerContact { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<Platform> Platform { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
