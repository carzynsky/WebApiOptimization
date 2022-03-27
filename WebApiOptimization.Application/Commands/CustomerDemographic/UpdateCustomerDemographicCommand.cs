using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.CustomerDemographic
{
    public class UpdateCustomerDemographicCommand : IRequest<CustomerDemographicResponse>
    {
        public string CustomerTypeId { get; set; }
        public string CustomerDesc { get; set; }
    }
}
