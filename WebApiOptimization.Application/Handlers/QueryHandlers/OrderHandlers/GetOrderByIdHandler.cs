using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.OrderQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Order
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, ResponseBuilder<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ResponseBuilder<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var orderEntity = _orderRepository.GetById(request.Id, true);
            if(orderEntity == null)
            {
                return new ResponseBuilder<OrderResponse> { Message = $"Order with id={request.Id} not found!", Data = null };
            }

            var response = OrderMapper.Mapper.Map<OrderResponse>(orderEntity);
            return new ResponseBuilder<OrderResponse> { Message = "OK.", Data = response };
        }
    }
}
