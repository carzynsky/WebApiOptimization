using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Filter;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.OrderDetailQueries
{
    public class GetOrderDetailQuery : PaginationFilter, IRequest<ResponseBuilder<IEnumerable<OrderDetailResponse>>>
    {
        public int? OrderID { get; set; }
        public int? ProductID { get; set; }
    }
}
