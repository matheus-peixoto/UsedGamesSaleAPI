using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsedGamesAPI.Models
{
    public class ClientContact
    {
        [Key]
        public int Id { get; set; }

        public string PhoneNumber { get; set; }

        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
    }
}
