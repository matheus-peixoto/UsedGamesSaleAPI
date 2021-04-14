using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UsedGamesAPI.Models.Enums;

namespace UsedGamesAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public int Quantity { get; set; }

        public int GameId { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }

        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
    }
}
