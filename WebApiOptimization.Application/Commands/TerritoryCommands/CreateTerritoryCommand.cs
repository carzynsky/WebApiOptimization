using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.TerritoryCommands
{
    public class CreateTerritoryCommand : IRequest<ResponseBuilder<TerritoryResponse>>
    {
        public string TerritoryID { get; set; }
        public string TerritoryDescription { get; set; }
        public int RegionId { get; set; }
    }
}
