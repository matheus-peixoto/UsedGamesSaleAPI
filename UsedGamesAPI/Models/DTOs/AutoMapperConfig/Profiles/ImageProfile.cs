using AutoMapper;
using UsedGamesAPI.Models.DTOs.Image;

namespace UsedGamesAPI.Models.DTOs.AutoMapperConfig.Profiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<CreateImageDTO, Models.Image>();
        }
    }
}
