using AutoMapper;
using UsedGamesAPI.DTOs.Sellers;
using UsedGamesAPI.Models;

namespace UsedGamesAPI.DTOs.AutoMapperConfig.Profiles
{
    public class SellerProfile : Profile
    {
        public SellerProfile()
        {
            CreateMap<CreateSellerDTO, Seller>();
            CreateMap<ContactForCreateSellerDTO, SellerContact>();
            CreateMap<UpdateSellerDTO, Seller>();
            CreateMap<Seller, UpdateSellerDTO>();
        }
    }
}
