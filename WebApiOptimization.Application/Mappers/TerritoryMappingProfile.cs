using AutoMapper;
using WebApiOptimization.Application.Commands.TerritoryCommands;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;

namespace WebApiOptimization.Application.Mappers
{
    public class TerritoryMappingProfile : Profile
    {
        public TerritoryMappingProfile()
        {
            CreateMap<Territory, TerritoryResponse>().ReverseMap();
            CreateMap<Territory, CreateTerritoryCommand>().ReverseMap();
            CreateMap<Territory, UpdateTerritoryCommand>().ReverseMap();
        }
    }
}
