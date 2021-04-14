using AutoMapper;
using UsedGamesAPI.DTOs.Order;

namespace UsedGamesAPI.DTOs.AutoMapperConfig.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderDTO, Models.Order>();
        }
    }
}
