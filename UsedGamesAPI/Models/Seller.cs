using System.Collections.Generic;

namespace UsedGamesAPI.Models
{
    public class Seller : User
    {
        public List<Game> Games { get; set; }
        public SellerContact SellerContact { get; set; }
    }
}
