using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsedGamesAPI.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Platform Platform { get; set; }
        public decimal Price { get; set; }
        public DateTime PosteDate { get; set; }
        public string GameDetails { get; set; }
    }
}
