using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.Supplier
{
    public class CreateSupplierCommand : IRequest<SupplierResponse>
    {
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTile { get; set; }
        public string ContactAddress { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string HomePage { get; set; }
    }
}
