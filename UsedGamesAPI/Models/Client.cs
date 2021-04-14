using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UsedGamesAPI.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Order> Orders { get; set; }
        public ClientContact ClientContact { get; set; }
    }
}
