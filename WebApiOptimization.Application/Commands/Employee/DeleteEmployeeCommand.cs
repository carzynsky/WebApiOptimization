using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.Employee
{
    public record DeleteEmployeeCommand(int Id) : IRequest<EmployeeResponse>;
}
