using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.Order;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Order
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, List<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository; 
        public GetAllOrdersHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<OrderResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = _orderRepository.GetAll();
            var response = OrderMapper.Mapper.Map<List<OrderResponse>>(orders);
            return response;
        }
    }
}
