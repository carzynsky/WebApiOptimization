using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Queries.OrderDetail;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.OrderDetail
{
    public class GetAllOrderDetailsHandler : IRequestHandler<GetAllOrderDetailsQuery, IEnumerable<OrderDetailResponse>>
    {
        private readonly IOrderDetailRepository _orderDetailRepository; 
        public GetAllOrderDetailsHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public Task<IEnumerable<OrderDetailResponse>> Handle(GetAllOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
