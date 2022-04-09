using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.OrderDetailCommands
{
    public record UpdateOrderDetailQueryParameter(int OrderId, int ProductId);
    public class UpdateOrderDetailCommand : IRequest<ResponseBuilder<OrderDetailResponse>>
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
    }
}
