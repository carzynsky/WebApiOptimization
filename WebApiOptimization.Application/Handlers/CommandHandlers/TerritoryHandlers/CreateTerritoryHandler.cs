using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.TerritoryCommands;
using WebApiOptimization.Application.Helpers;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.TerritoryHandlers
{
    public class CreateTerritoryHandler : IRequestHandler<CreateTerritoryCommand, ResponseBuilder<TerritoryResponse>>
    {
        private readonly ITerritoryRepository _territoryRepository;

        public CreateTerritoryHandler(ITerritoryRepository territoryRepository)
        {
            _territoryRepository = territoryRepository;
        }

        public async Task<ResponseBuilder<TerritoryResponse>> Handle(CreateTerritoryCommand request, CancellationToken cancellationToken)
        {
            var territoryEntity = TerritoryMapper.Mapper.Map<Territory>(request);
            if(territoryEntity == null)
            {
                return new ResponseBuilder<TerritoryResponse> { Message = ResponseBuilderHelper.InvalidData, Data = null };
            }

            try
            {
                var newTerritory = _territoryRepository.Add(territoryEntity);
                var response = TerritoryMapper.Mapper.Map<TerritoryResponse>(newTerritory);
                return new ResponseBuilder<TerritoryResponse> { Message = "Territory created.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<TerritoryResponse> { Message = $"Territory not created! Error: {e.Message}", Data = null };
            }
           
        }
    }
}
