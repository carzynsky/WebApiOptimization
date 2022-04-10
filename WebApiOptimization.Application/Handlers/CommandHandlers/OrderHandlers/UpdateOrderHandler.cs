using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.OrderCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.OrderHandlers
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, ResponseBuilder<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ResponseBuilder<OrderResponse>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = _orderRepository.GetById(request.OrderId);
            if (orderToUpdate == null)
            {
                return new ResponseBuilder<OrderResponse> { Message = $"Order with id={request.OrderId} not found!", Data = null };
            }

            try
            {
                var orderToUpdateEntity = OrderMapper.Mapper.Map<Order>(request);
                _orderRepository.Update(orderToUpdateEntity);
                var response = OrderMapper.Mapper.Map<OrderResponse>(orderToUpdateEntity);
                return new ResponseBuilder<OrderResponse> { Message = "Order updated.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<OrderResponse> { Message = $"Order not updated! Error: {e.InnerException.Message}", Data = null };
            }
        }
    }
}
