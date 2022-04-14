using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Queries.OrderDetailQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Application.Mappers;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.OrderDetailHandlers
{
    public class GetOrderDetailHandler : IRequestHandler<GetOrderDetailQuery, ResponseBuilder<IEnumerable<OrderDetailResponse>>>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public GetOrderDetailHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task<ResponseBuilder<IEnumerable<OrderDetailResponse>>> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<OrderDetail> orderDetails;
            if(request.OrderId == null && request.ProductId == null)
            {
                orderDetails = await _orderDetailRepository.GetAllAsync();
            }
            else if(request.OrderId != null && request.ProductId == null)
            {
                orderDetails = await _orderDetailRepository.GetByOrderIdAsync((int)request.OrderId, true);
            }
            else if(request.OrderId == null && request.ProductId != null)
            {
                orderDetails = await _orderDetailRepository.GetByProductIdAsync((int)request.ProductId, true);
            }
            else
            {
                orderDetails = await _orderDetailRepository.GetByOrderIdAndProductIdAsync((int)request.OrderId, (int)request.ProductId, true);
            }

            var response = OrderDetailMapper.Mapper.Map<IEnumerable<OrderDetailResponse>>(orderDetails);
            return new ResponseBuilder<IEnumerable<OrderDetailResponse>> { Message = "OK.", Data = response };
        }
    }
}
