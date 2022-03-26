using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.OrderDetail;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.OrderDetailHandlers
{
    public class CreateOrderDetailHandler : IRequestHandler<CreateOrderDetailCommand, OrderDetailResponse>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public CreateOrderDetailHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task<OrderDetailResponse> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderDetailEntity = OrderDetailMapper.Mapper.Map<OrderDetail>(request);
            if(orderDetailEntity == null)
            {
                return null;
            }

            var newOrderDetail = _orderDetailRepository.Add(orderDetailEntity);
            var response = OrderDetailMapper.Mapper.Map<OrderDetailResponse>(newOrderDetail);
            return response;
        }
    }
}
