using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UsedGamesAPI.Models
{
    public class Seller
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Game> Games { get; set; }
        public SellerContact SellerContact { get; set; }
    }
}
