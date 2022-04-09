using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.OrderDetailCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.OrderDetailHandlers
{
    public class UpdateOrderDetailHandler : IRequestHandler<UpdateOrderDetailCommand, ResponseBuilder<OrderDetailResponse>>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public UpdateOrderDetailHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<ResponseBuilder<OrderDetailResponse>> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderDetailToUpdate = _orderDetailRepository.GetByOrderIdAndProductId(request.OrderId, request.ProductId);
            if (orderDetailToUpdate == null)
            {
                return new ResponseBuilder<OrderDetailResponse> { Message = $"OrderDetail with orderId={request.OrderId} not found!", Data = null };
            }

            var orderDetailToUpdateEntity = OrderDetailMapper.Mapper.Map<OrderDetail>(request);
            _orderDetailRepository.Update(orderDetailToUpdateEntity);
            var response = OrderDetailMapper.Mapper.Map<OrderDetailResponse>(orderDetailToUpdateEntity);
            return new ResponseBuilder<OrderDetailResponse> { Message = "OrderDetail updated!", Data = response };
        }
    }
}
