using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.OrderQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Application.Wrappers;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.OrderHandler
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
            List<Order> orders;
            List<OrderResponse> ordersDto;

            if(request.PageNumber == 0 || request.PageSize == 0)
            {
                orders = await _orderRepository.GetAllAsync();
                ordersDto = OrderMapper.Mapper.Map<List<OrderResponse>>(orders);
                return new ResponseBuilder<IEnumerable<OrderResponse>> { Data = ordersDto, Message = "OK" };
            }
            
            orders = await _orderRepository.GetAllPagedAsync(request.PageNumber, request.PageSize);
            ordersDto = OrderMapper.Mapper.Map<List<OrderResponse>>(orders);
            return new PagedResponse<IEnumerable<OrderResponse>>(ordersDto, request.PageNumber, request.PageSize, "OK");
        }
    }
}
