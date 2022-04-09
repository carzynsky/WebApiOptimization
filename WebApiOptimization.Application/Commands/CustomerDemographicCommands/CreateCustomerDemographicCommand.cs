using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.CustomerDemographicCommands
{
    public class CreateCustomerDemographicCommand : IRequest<ResponseBuilder<CustomerDemographicResponse>>
    {
        public string CustomerTypeId { get; set; }
        public string CustomerDesc { get; set; }
    }
}
