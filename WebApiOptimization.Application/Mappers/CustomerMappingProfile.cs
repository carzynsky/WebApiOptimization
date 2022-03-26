using AutoMapper;
using WebApiOptimization.Application.Commands.Customer;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;

namespace WebApiOptimization.Application.Mappers
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerResponse>().ReverseMap();
            CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
            CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();
        }
    }
}
