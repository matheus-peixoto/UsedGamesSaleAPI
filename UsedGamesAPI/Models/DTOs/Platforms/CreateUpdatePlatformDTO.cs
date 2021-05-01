using System.ComponentModel.DataAnnotations;

namespace UsedGamesAPI.DTOs.Platforms
{
    public class CreateUpdatePlatformDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
