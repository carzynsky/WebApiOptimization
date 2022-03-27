using WebApiOptimization.Core.Entities;

namespace WebApiOptimization.Application.Responses
{
    public class TerritoryResponse
    {
        public string TerritoryID { get; set; }
        public string TerritoryDescription { get; set; }
        public int RegionID { get; set; }
        public Region Region { get; set; }
    }
}
