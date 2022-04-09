using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.TerritoryCommands
{
    public record DeleteTerritoryCommand(string Id) : IRequest<ResponseBuilder<TerritoryResponse>>;
}
