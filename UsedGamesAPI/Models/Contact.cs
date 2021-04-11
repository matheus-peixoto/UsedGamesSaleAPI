using System.ComponentModel.DataAnnotations;

namespace UsedGamesAPI.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
    }
}
