using AutoMapper;
using WebApiOptimization.Application.Commands.Supplier;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;

namespace WebApiOptimization.Application.Mappers
{
    public class SupplierMappingProfile : Profile
    {
        public SupplierMappingProfile()
        {
            CreateMap<Supplier, SupplierResponse>().ReverseMap();
            CreateMap<Supplier, CreateSupplierCommand>().ReverseMap();
            CreateMap<Supplier, UpdateSupplierCommand>().ReverseMap();
        }
    }
}
