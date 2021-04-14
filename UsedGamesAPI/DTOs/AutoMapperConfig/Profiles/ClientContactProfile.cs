using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.ClientContacts;
using UsedGamesAPI.Models;

namespace UsedGamesAPI.DTOs.AutoMapperConfig.Profiles
{
    public class ClientContactProfile : Profile
    {
        public ClientContactProfile()
        {
            CreateMap<CreateClientContactDTO, ClientContact>();
        }
    }
}
