using UsedGamesAPI.Models.Enums;

namespace UsedGamesAPI.DTOs.Order
{
    public class CreateOrderDTO
    {
        public OrderStatus Status { get; set; }
        public int StockQuantity { get; set; }
        public int ClientId { get; set; }
        public int GameId { get; set; }
    }
}
