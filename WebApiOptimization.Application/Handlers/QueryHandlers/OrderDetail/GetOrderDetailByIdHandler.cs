using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.OrderDetail;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.OrderDetail
{
    public class GetOrderDetailByIdHandler : IRequestHandler<GetOrderDetailByIdQuery, OrderDetailResponse>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public GetOrderDetailByIdHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task<OrderDetailResponse> Handle(GetOrderDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var orderDetailEntity = _orderDetailRepository.GetById(request.Id);
            var response = OrderDetailMapper.Mapper.Map<OrderDetailResponse>(orderDetailEntity);
            return response;
        }
    }
}
