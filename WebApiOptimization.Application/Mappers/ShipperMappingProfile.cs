using AutoMapper;
using WebApiOptimization.Application.Commands.Shipper;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;

namespace WebApiOptimization.Application.Mappers
{
    public class ShipperMappingProfile : Profile
    {
        public ShipperMappingProfile()
        {
            CreateMap<Shipper, ShipperResponse>().ReverseMap();
            CreateMap<Shipper, CreateShipperCommand>().ReverseMap();
            CreateMap<Shipper, UpdateShipperCommand>().ReverseMap();
        }
    }
}
