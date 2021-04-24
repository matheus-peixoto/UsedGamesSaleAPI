using System.ComponentModel.DataAnnotations;

namespace UsedGamesAPI.Models
{
    public abstract class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
