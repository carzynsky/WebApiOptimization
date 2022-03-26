using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.OrderDetail;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.OrderDetailHandlers
{
    public class UpdateOrderDetailHandler : IRequestHandler<UpdateOrderDetailCommand, OrderDetailResponse>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public UpdateOrderDetailHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<OrderDetailResponse> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderDetailToUpdate = _orderDetailRepository.GetById(request.OrderId);
            if (orderDetailToUpdate == null)
            {
                return null;
            }

            var orderDetailToUpdateEntity = OrderDetailMapper.Mapper.Map<OrderDetail>(request);
            if (orderDetailToUpdateEntity == null)
            {
                return null;
            }

            _orderDetailRepository.Update(orderDetailToUpdateEntity);
            var response = OrderDetailMapper.Mapper.Map<OrderDetailResponse>(orderDetailToUpdateEntity);
            return response;
        }
    }
}
