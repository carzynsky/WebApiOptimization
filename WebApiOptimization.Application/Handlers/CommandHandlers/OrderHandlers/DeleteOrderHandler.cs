using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.OrderCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.OrderHandlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, ResponseBuilder<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public DeleteOrderHandler(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<ResponseBuilder<OrderResponse>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = _orderRepository.GetById(request.Id);
            if(orderToDelete == null)
            {
                return new ResponseBuilder<OrderResponse> { Message = $"Order with id={request.Id} not found!", Data = null };
            }

            try
            {
                // Find order details with this orderId
                var orderDetailsWithThisOrderId = _orderDetailRepository.GetByOrderId(request.Id).ToList();
                if (orderDetailsWithThisOrderId.Any())
                {
                    // Remove entries
                    _orderDetailRepository.DeleteRange(orderDetailsWithThisOrderId);
                }

                _orderRepository.Delete(orderToDelete);
                var response = OrderMapper.Mapper.Map<OrderResponse>(orderToDelete);
                return new ResponseBuilder<OrderResponse> { Message = "Order deleted.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<OrderResponse> { Message = $"Order not deleted! Error: {e.InnerException.Message}", Data = null };
            }
        }
    }
}
