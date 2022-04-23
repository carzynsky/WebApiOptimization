using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.TerritoryCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.TerritoryHandlers
{
    public class UpdateTerritoryHandler : IRequestHandler<UpdateTerritoryCommand, ResponseBuilder<TerritoryResponse>>
    {
        private readonly ITerritoryRepository _territoryRepository;

        public UpdateTerritoryHandler(ITerritoryRepository territoryRepository)
        {
            _territoryRepository = territoryRepository;
        }

        public async Task<ResponseBuilder<TerritoryResponse>> Handle(UpdateTerritoryCommand request, CancellationToken cancellationToken)
        {
            var territoryToUpdate = await _territoryRepository.GetByIdAsync(request.TerritoryId);
            if (territoryToUpdate == null)
            {
                return new ResponseBuilder<TerritoryResponse> { Message = $"Territory with id={request.TerritoryId} not found!", Data = null };
            }

            try
            {
                var territoryToUpdateEntity = TerritoryMapper.Mapper.Map<Territory>(request);
                _territoryRepository.Update(territoryToUpdateEntity);
                var response = TerritoryMapper.Mapper.Map<TerritoryResponse>(territoryToUpdateEntity);
                return new ResponseBuilder<TerritoryResponse> { Message = "Territory updated.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<TerritoryResponse> { Message = $"Territory not updated! Error: {e.InnerException.Message}", Data = null };
            }
        }
    }
}
