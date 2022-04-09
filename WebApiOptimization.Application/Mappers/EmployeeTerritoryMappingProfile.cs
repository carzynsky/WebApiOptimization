using AutoMapper;
using WebApiOptimization.Application.Commands.EmployeeTerritoryCommands;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;

namespace WebApiOptimization.Application.Mappers
{
    public class EmployeeTerritoryMappingProfile : Profile
    {
        public EmployeeTerritoryMappingProfile()
        {
            CreateMap<EmployeeTerritory, EmployeeTerritoryResponse>().ReverseMap();
            CreateMap<EmployeeTerritory, CreateEmployeeTerritoryCommand>().ReverseMap();
        }
    }
}
