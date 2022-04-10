using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.EmployeeTerritoryCommands;
using WebApiOptimization.Application.Helpers;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.EmployeeTerritoryHandlers
{
    public class CreateEmployeeTerritoryHandler : IRequestHandler<CreateEmployeeTerritoryCommand, ResponseBuilder<EmployeeTerritoryResponse>>
    {
        private readonly IEmployeeTerritoryRepository _employeeTerritoryRepository;

        public CreateEmployeeTerritoryHandler(IEmployeeTerritoryRepository employeeTerritoryRepository)
        {
            _employeeTerritoryRepository = employeeTerritoryRepository;
        }

        public async Task<ResponseBuilder<EmployeeTerritoryResponse>> Handle(CreateEmployeeTerritoryCommand request, CancellationToken cancellationToken)
        {
            var employeeTerritoryEntity = EmployeeTerritoryMapper.Mapper.Map<EmployeeTerritory>(request);
            if (employeeTerritoryEntity == null)
            {
                return new ResponseBuilder<EmployeeTerritoryResponse> { Message = ResponseBuilderHelper.InvalidData, Data = null };
            }

            try
            {
                var newEmployeeTerritory = _employeeTerritoryRepository.Add(employeeTerritoryEntity);
                var response = EmployeeTerritoryMapper.Mapper.Map<EmployeeTerritoryResponse>(newEmployeeTerritory);
                return new ResponseBuilder<EmployeeTerritoryResponse> { Message = "EmployeeTerritory created.", Data = response };
            }
            catch (Exception e)
            {
                return new ResponseBuilder<EmployeeTerritoryResponse> { Message = $"EmployeeTerritory not created! Error: {e.InnerException.Message}", Data = null };
            }
        }
    }
}
