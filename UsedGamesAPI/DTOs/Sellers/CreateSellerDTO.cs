using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.Models;

namespace UsedGamesAPI.DTOs.Sellers
{
    public class CreateSellerDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ContactForCreateSellerDTO Contact { get; set; }
    }
}
