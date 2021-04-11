using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UsedGamesAPI.Models
{
    public class Platform
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Game> Games { get; set; }
    }
}
