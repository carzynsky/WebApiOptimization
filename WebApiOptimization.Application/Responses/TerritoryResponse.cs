using WebApiOptimization.Core.Entities;

namespace WebApiOptimization.Application.Responses
{
    public class TerritoryResponse
    {
        public int TerritoryId { get; set; }
        public string TerritoryDescription { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
