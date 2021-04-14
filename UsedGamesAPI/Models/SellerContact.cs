using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsedGamesAPI.Models
{
    public class SellerContact
    {
        [Key]
        public int Id { get; set; }
        public string PhoneNumber { get; set; }

        public int SellerId { get; set; }
        [ForeignKey("SellerId")]
        public Seller Seller { get; set; }
    }
}
