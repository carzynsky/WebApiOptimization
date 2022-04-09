using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.OrderDetailCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;
using System.Linq;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.OrderDetailHandlers
{
    public class DeleteOrderDetailHandler : IRequestHandler<DeleteOrderDetailCommand, ResponseBuilder<List<OrderDetailResponse>>>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public DeleteOrderDetailHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<ResponseBuilder<List<OrderDetailResponse>>> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderDetailsToRemove = _orderDetailRepository.GetByOrderId(request.OrderID).ToList();
            if(!orderDetailsToRemove.Any())
            {
                return new ResponseBuilder<List<OrderDetailResponse>> { Message = $"OrderDetails with orderId={request.OrderID} not found!", Data = null };
            }

            _orderDetailRepository.DeleteRange(orderDetailsToRemove);
            var response = OrderDetailMapper.Mapper.Map<List<OrderDetailResponse>>(orderDetailsToRemove);
            return new ResponseBuilder<List<OrderDetailResponse>> { Message = "OrderDetails deleted.", Data = response };
        }
    }
}
