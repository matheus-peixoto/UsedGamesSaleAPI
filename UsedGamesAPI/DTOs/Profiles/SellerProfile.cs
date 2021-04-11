using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.Sellers;
using UsedGamesAPI.Models;

namespace UsedGamesAPI.DTOs.Profiles
{
    public class SellerProfile : Profile
    {
        public SellerProfile()
        {
            CreateMap<CreateSellerDTO, Seller>();
            CreateMap<UpdateSellerDTO, Seller>();
            CreateMap<Seller, UpdateSellerDTO>();
        }
    }
}
