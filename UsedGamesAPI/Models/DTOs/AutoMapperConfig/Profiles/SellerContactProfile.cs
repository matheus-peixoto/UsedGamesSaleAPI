using AutoMapper;
using UsedGamesAPI.DTOs.SellerContacts;
using UsedGamesAPI.Models;

namespace UsedGamesAPI.DTOs.AutoMapperConfig.Profiles
{
    public class SellerContactProfile : Profile
    {
        public SellerContactProfile()
        {
            CreateMap<CreateSellerContactDTO, SellerContact>();
            CreateMap<SellerContact, UpdateSellerContactDTO>();
            CreateMap<UpdateSellerContactDTO, SellerContact>();
        }
    }
}
