using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Order;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.OrderHandlers
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        public UpdateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<OrderResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = _orderRepository.GetById(request.OrderId);
            if (orderToUpdate == null)
            {
                return null;
            }

            var orderToUpdateEntity = OrderMapper.Mapper.Map<Order>(request);
            if (orderToUpdateEntity == null)
            {
                return null;
            }

            _orderRepository.Update(orderToUpdateEntity);
            var response = OrderMapper.Mapper.Map<OrderResponse>(orderToUpdateEntity);
            return response;
        }
    }
}
