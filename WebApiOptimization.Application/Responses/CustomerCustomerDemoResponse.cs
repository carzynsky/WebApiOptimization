using WebApiOptimization.Core.Entities;

namespace WebApiOptimization.Application.Responses
{
    public class CustomerCustomerDemoResponse
    {
        public string CustomerID { get; set; }
        public Customer Customer { get; set; }
        public string CustomerTypeID { get; set; }
        public CustomerDemographic CustomerDemographic { get; set; }
    }
}
