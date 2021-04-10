using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.Models.Enums;

namespace UsedGamesAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<Game> Games { get; set; }
        public Seller Seller { get; set; }
        public Client Client { get; set; }
        public OrderStatus Status { get; set; }
    }
}
