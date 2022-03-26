using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.OrderDetail;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.OrderDetailHandlers
{
    public class DeleteOrderDetailHandler : IRequestHandler<DeleteOrderDetailCommand, OrderDetailResponse>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public DeleteOrderDetailHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task<OrderDetailResponse> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderDetailToRemove = _orderDetailRepository.GetById(request.Id);
            if(orderDetailToRemove == null)
            {
                return null;
            }

            _orderDetailRepository.Delete(orderDetailToRemove);
            var response = OrderDetailMapper.Mapper.Map<OrderDetailResponse>(orderDetailToRemove);
            return response;
        }
    }
}
