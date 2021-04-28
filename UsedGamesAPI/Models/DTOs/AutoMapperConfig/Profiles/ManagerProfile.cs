using AutoMapper;
using UsedGamesAPI.DTOs.Manager;

namespace UsedGamesAPI.DTOs.AutoMapperConfig.Profiles
{
    public class ManagerProfile : Profile
    {
        public ManagerProfile()
        {
            CreateMap<CreateManagerDTO, Models.Manager>();
        }
    }
}
