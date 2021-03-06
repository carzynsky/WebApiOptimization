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
                employeeTerritories = await _employeeTerritoryRepository.GetByEmployeeIdAndTerritoryIdAsync((int)request.EmployeeId, request.TerritoryId);
            }
            else if(request.EmployeeId != null && request.TerritoryId == null)
            {
                employeeTerritories = await _employeeTerritoryRepository.GetByEmployeeIdAsync((int)request.EmployeeId);
            }
            else
            {
                employeeTerritories = await _employeeTerritoryRepository.GetByTerritoryIdAsync(request.TerritoryId);
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
