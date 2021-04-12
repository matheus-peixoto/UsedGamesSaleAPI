using AutoMapper;
using UsedGamesAPI.DTOs.Platforms;
using UsedGamesAPI.Models;

namespace UsedGamesAPI.DTOs.AutoMapperConfig.Profiles
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            CreateMap<CreateUpdatePlatformDTO, Platform>();
        }
    }
}
