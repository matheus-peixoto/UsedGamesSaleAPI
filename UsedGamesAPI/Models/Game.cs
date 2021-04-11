using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsedGamesAPI.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime PosteDate { get; set; }
        public string GameDetails { get; set; }
        public Platform Platform { get; set; }
    }
}
