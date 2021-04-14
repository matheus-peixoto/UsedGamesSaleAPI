using System.ComponentModel.DataAnnotations;
using UsedGamesAPI.Models.Enums;

namespace UsedGamesAPI.DTOs.Order
{
    public class CreateOrderDTO
    {
        public OrderStatus Status { get; set; }
        public int Quantity { get; set; }
        public int ClientId { get; set; }
        public int SellerId { get; set; }
    }
}
