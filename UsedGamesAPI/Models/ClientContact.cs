using System.ComponentModel.DataAnnotations;

namespace UsedGamesAPI.Models
{
    public class ClientContact
    {
        [Key]
        public int Id { get; set; }

        public string PhoneNumber { get; set; }
    }
}
