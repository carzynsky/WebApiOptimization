using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.TerritoryCommands
{
    public class UpdateTerritoryCommand : IRequest<ResponseBuilder<TerritoryResponse>>
    {
        public string TerritoryId { get; set; }
        public string TerritoryDescription { get; set; }
        public int RegionId { get; set; }
    }
}
