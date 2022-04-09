using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.OrderCommands;
using WebApiOptimization.Application.Helpers;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.OrderHandlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, ResponseBuilder<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ResponseBuilder<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = OrderMapper.Mapper.Map<Order>(request);
            if(orderEntity == null)
            {
                return new ResponseBuilder<OrderResponse> { Message = ResponseBuilderHelper.InvalidData, Data = null };
            }

            try
            {
                var newOrder = _orderRepository.Add(orderEntity);
                var response = OrderDetailMapper.Mapper.Map<OrderResponse>(newOrder);
                return new ResponseBuilder<OrderResponse> { Message = "Order created.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<OrderResponse> { Message = $"Order not created! Error: {e.Message}", Data = null };
            }
        }
    }
}
