using WebApiOptimization.Core.Entities;

namespace WebApiOptimization.Application.Responses
{
    public class CustomerCustomerDemoResponse
    {
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public int CustomerTypeID { get; set; }
        public CustomerDemographic CustomerDemographic { get; set; }
    }
}
