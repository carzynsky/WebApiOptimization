using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Queries.OrderDetailQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Wrappers;

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

            if(request.OrderID != null && request.ProductID == null)
            {
                orderDetails = await _orderDetailRepository.GetByOrderIdAsync((int)request.OrderID, true);
            }
            else if(request.OrderID == null && request.ProductID != null)
            {
                orderDetails = await _orderDetailRepository.GetByProductIdAsync((int)request.ProductID, true);
            }
            else if(request.OrderID != null && request.ProductID != null)
            {
                orderDetails = await _orderDetailRepository.GetByOrderIdAndProductIdAsync((int)request.OrderID, (int)request.ProductID, true);
            }
            else if (request.PageNumber != 0 && request.PageSize != 0)
            {
                orderDetails = await _orderDetailRepository.GetAllPagedAsync(request.PageNumber, request.PageSize);
                var orderDetailsDto = OrderDetailMapper.Mapper.Map<IEnumerable<OrderDetailResponse>>(orderDetails);
                return new PagedResponse<IEnumerable<OrderDetailResponse>>(orderDetailsDto, request.PageNumber, request.PageSize, "OK");
            }
            else
            {
                orderDetails = await _orderDetailRepository.GetAllAsync();
            }

            var response = OrderDetailMapper.Mapper.Map<IEnumerable<OrderDetailResponse>>(orderDetails);
            return new ResponseBuilder<IEnumerable<OrderDetailResponse>> { Message = "OK.", Data = response };
        }
    }
}
