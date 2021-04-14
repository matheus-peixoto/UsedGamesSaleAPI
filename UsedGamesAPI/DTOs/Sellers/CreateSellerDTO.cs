namespace UsedGamesAPI.DTOs.Sellers
{
    public class CreateSellerDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ContactForCreateSellerDTO SellerContact { get; set; }
    }
}
