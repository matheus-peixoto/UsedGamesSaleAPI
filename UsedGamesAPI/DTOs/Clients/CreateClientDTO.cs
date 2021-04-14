namespace UsedGamesAPI.DTOs.Clients
{
    public class CreateClientDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ContactForCreateClientDTO ClientContact { get; set; }
    }
}
