using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.Supplier
{
    public record GetSupplierByIdQuery(int Id) : IRequest<SupplierResponse>;
}
