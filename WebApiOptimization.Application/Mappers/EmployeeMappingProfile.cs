using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;

namespace WebApiOptimization.Application.Mappers
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeResponse>().ReverseMap();
            CreateMap<Employee, CreateEmployeeCommand>().ReverseMap();
        }
    }
}
