using AutoMapper;
using WebApiOptimization.Application.Commands.CustomerCustomerDemo;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;

namespace WebApiOptimization.Application.Mappers
{
    public class CustomerCustomerDemoMappingProfile : Profile
    {
        public CustomerCustomerDemoMappingProfile()
        {
            CreateMap<CustomerCustomerDemo, CustomerCustomerDemoResponse>().ReverseMap();
            CreateMap<CustomerCustomerDemo, CreateCustomerCustomerDemoCommand>().ReverseMap();
            CreateMap<CustomerCustomerDemo, UpdateCustomerCustomerDemoCommand>().ReverseMap();
        }
    }
}
