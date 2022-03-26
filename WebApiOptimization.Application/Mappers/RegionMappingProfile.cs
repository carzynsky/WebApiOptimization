using AutoMapper;
using WebApiOptimization.Application.Commands.Region;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;

namespace WebApiOptimization.Application.Mappers
{
    public class RegionMappingProfile : Profile
    {
        public RegionMappingProfile()
        {
            CreateMap<Region, RegionResponse>().ReverseMap();
            CreateMap<Region, CreateRegionCommand>().ReverseMap();
            CreateMap<Region, UpdateRegionCommand>().ReverseMap();
        }
    }
}
