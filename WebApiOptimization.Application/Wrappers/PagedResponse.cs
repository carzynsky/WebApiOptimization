using System;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Wrappers
{
    public class PagedResponse<T> : ResponseBuilder<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PagedResponse(T data, int pageNumber, int pageSize, string message)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
            Message = message;
        }
    }
}
