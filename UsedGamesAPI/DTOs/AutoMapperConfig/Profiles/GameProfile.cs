using AutoMapper;
using UsedGamesAPI.DTOs.Games;
using UsedGamesAPI.Models;

namespace UsedGamesAPI.DTOs.AutoMapperConfig.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<CreateGameDTO, Game>();
            CreateMap<UpdateGameDTO, Game>();
            CreateMap<Game, UpdateGameDTO>();
        }
    }
}
