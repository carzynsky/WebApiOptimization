using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.OrderDetailCommands;
using WebApiOptimization.Application.Helpers;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.OrderDetailHandlers
{
    public class CreateOrderDetailHandler : IRequestHandler<CreateOrderDetailCommand, ResponseBuilder<OrderDetailResponse>>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public CreateOrderDetailHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<ResponseBuilder<OrderDetailResponse>> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderDetailEntity = OrderDetailMapper.Mapper.Map<OrderDetail>(request);
            if(orderDetailEntity == null)
            {
                return new ResponseBuilder<OrderDetailResponse> { Message = ResponseBuilderHelper.InvalidData, Data = null };
            }

            try
            {
                var newOrderDetail = await _orderDetailRepository.AddAsync(orderDetailEntity);
                var response = OrderDetailMapper.Mapper.Map<OrderDetailResponse>(newOrderDetail);
                return new ResponseBuilder<OrderDetailResponse> { Message = "OrderDetail created.", Data = response };
            }
            catch (Exception e)
            {
                return new ResponseBuilder<OrderDetailResponse> { Message = $"OrderDetail not created. Error: {e.InnerException.Message}", Data = null };
            }
        }
    }
}
