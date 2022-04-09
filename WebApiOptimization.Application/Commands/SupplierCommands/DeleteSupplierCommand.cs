using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.SupplierCommands
{
    public record DeleteSupplierCommand(int Id) : IRequest<ResponseBuilder<SupplierResponse>>;
}
