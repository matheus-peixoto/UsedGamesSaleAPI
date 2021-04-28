using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsedGamesAPI.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime PostDate { get; set; }
        public string Details { get; set; }
        public int StockQuantity { get; set; }

        public List<Order> Orders { get; set; }

        public List<Image> Images { get; set; }

        public int SellerId { get; set; }
        [ForeignKey("SellerId")]
        public Seller Seller { get; set; }

        public int PlatformId { get; set; }

        [ForeignKey("PlatformId")]
        public Platform Platform { get; set; }
    }
}