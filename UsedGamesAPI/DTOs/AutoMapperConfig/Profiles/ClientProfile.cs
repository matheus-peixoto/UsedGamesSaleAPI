using AutoMapper;
using UsedGamesAPI.DTOs.Clients;
using UsedGamesAPI.Models;

namespace UsedGamesAPI.DTOs.AutoMapperConfig.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<CreateClientDTO, Client>();
            CreateMap<UpdateClientDTO, Client>();
            CreateMap<Client, UpdateClientDTO>();
        }
    }
}
