using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsedGamesAPI.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Contact Contact { get; set; }
        public List<Order> Orders { get; set; }
    }
}
