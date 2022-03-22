using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.Supplier
{
    public record DeleteSupplierCommand : IRequest<SupplierResponse>;
}
