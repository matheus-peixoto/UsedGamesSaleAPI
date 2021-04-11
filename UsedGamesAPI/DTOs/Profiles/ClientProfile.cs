using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.Clients;
using UsedGamesAPI.Models;

namespace UsedGamesAPI.DTOs.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<CreateClientDTO, Client>();
            CreateMap<Client, CreateClientDTO>();
        }
    }
}
