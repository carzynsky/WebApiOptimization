using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.SupplierQueries
{
    public record GetSupplierByIdQuery(int Id) : IRequest<ResponseBuilder<SupplierResponse>>;
}
