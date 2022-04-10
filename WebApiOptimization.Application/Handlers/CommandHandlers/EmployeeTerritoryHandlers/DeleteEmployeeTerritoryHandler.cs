using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.EmployeeTerritoryCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.EmployeeTerritoryHandlers
{
    public class DeleteEmployeeTerritoryHandler : IRequestHandler<DeleteEmployeeTerritoryCommand, ResponseBuilder<List<EmployeeTerritoryResponse>>>
    {
        private readonly IEmployeeTerritoryRepository _employeeTerritoryRepository;

        public DeleteEmployeeTerritoryHandler(IEmployeeTerritoryRepository employeeTerritoryRepository)
        {
            _employeeTerritoryRepository = employeeTerritoryRepository;
        }

        public async Task<ResponseBuilder<List<EmployeeTerritoryResponse>>> Handle(DeleteEmployeeTerritoryCommand request, CancellationToken cancellationToken)
        {
            List<EmployeeTerritory> employeeTerritories;
            if(request.EmployeeId != null && request.TerritoryId != null)
            {
                employeeTerritories = _employeeTerritoryRepository.GetByEmployeeIdAndTerritoryId((int)request.EmployeeId, request.TerritoryId).ToList();
            }
            else if(request.EmployeeId != null && request.TerritoryId == null)
            {
                employeeTerritories = _employeeTerritoryRepository.GetByEmployeeId((int)request.EmployeeId).ToList();
            }
            else
            {
                employeeTerritories = _employeeTerritoryRepository.GetByTerritoryId(request.TerritoryId).ToList();
            }

            try
            {
                _employeeTerritoryRepository.DeleteRange(employeeTerritories);
                var response = EmployeeMapper.Mapper.Map<List<EmployeeTerritoryResponse>>(employeeTerritories);
                return new ResponseBuilder<List<EmployeeTerritoryResponse>> { Message = "EmployeTerritories deleted.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<List<EmployeeTerritoryResponse>> { Message = $"EmployeTerritories not deleted! Error: {e.InnerException.Message}", Data = null };
            }
        }
    }
}
