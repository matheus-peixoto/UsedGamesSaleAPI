using System;

namespace UsedGamesAPI.DTOs.Games
{
    public class CreateGameDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime PostDate { get; set; }
        public string Details { get; set; }
        public int PlatformId { get; set; }
        public int SellerId { get; set; }
    }
}
