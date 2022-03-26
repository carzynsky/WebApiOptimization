using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Order;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.OrderHandlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        public DeleteOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<OrderResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = _orderRepository.GetById(request.Id);
            if(orderToDelete == null)
            {
                return null;
            }

            _orderRepository.Delete(orderToDelete);
            var response = OrderMapper.Mapper.Map<OrderResponse>(orderToDelete);
            return response;
        }
    }
}
