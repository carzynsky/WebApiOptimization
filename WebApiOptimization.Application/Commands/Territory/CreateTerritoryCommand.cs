using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.Territory
{
    public class CreateTerritoryCommand : IRequest<TerritoryResponse>
    {
        public string TerritoryDescription { get; set; }
        public int RegionId { get; set; }
    }
}
