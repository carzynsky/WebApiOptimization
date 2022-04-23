using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.OrderQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Order
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, ResponseBuilder<IEnumerable<OrderResponse>>>
    {
        private readonly IOrderRepository _orderRepository; 

        public GetAllOrdersHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ResponseBuilder<IEnumerable<OrderResponse>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync();
            var response = OrderMapper.Mapper.Map<IEnumerable<OrderResponse>>(orders);
            return new ResponseBuilder<IEnumerable<OrderResponse>> { Message = "OK.", Data = response };
        }
    }
}
