using System.ComponentModel.DataAnnotations.Schema;

namespace UsedGamesAPI.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }

        public int GameId { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }
    }
}
