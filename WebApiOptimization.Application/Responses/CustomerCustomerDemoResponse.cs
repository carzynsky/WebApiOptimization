using WebApiOptimization.Core.Entities;

namespace WebApiOptimization.Application.Responses
{
    public class CustomerCustomerDemoResponse
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerTypeId { get; set; }
        public CustomerDemographic CustomerDemographic { get; set; }
    }
}
