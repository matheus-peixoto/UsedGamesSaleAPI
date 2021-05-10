namespace UsedGamesAPI.DTOs.Games
{
    public class UpdateGameDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Details { get; set; }
        public int StockQuantity { get; set; }
        public int PlatformId { get; set; }
    }
}
