using AutoMapper;
using WebApiOptimization.Application.Commands.CustomerDemographic;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;

namespace WebApiOptimization.Application.Mappers
{
    public class CustomerDemographicMappingProfile : Profile
    {
        public CustomerDemographicMappingProfile()
        {
            CreateMap<CustomerDemographic, CustomerDemographicResponse>().ReverseMap();
            CreateMap<CustomerDemographic, CreateCustomerDemographicCommand>().ReverseMap();
            CreateMap<CustomerDemographic, UpdateCustomerDemographicCommand>().ReverseMap();
        }
    }
}
