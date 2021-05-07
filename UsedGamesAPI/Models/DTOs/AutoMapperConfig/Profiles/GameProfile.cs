using AutoMapper;
using UsedGamesAPI.DTOs.Games;
using UsedGamesAPI.Models;
using UsedGamesAPI.Models.DTOs.Games;

namespace UsedGamesAPI.DTOs.AutoMapperConfig.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<CreateGameDTO, Game>();
            CreateMap<ImageForCreateGameDTO, Image>();
            CreateMap<UpdateGameDTO, Game>();
            CreateMap<Game, UpdateGameDTO>();
        }
    }
}
