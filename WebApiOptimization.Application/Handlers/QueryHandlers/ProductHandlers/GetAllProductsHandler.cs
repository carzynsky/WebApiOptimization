using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.ProductQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Application.Wrappers;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.ProductHandlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, ResponseBuilder<IEnumerable<ProductResponse>>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResponseBuilder<IEnumerable<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            List<Product> products;
            List<ProductResponse> productsDto;

            if(request.PageNumber == 0 || request.PageSize == 0)
            {
                products = await _productRepository.GetAllAsync();
                productsDto = ProductMapper.Mapper.Map<List<ProductResponse>>(products);
                return new ResponseBuilder<IEnumerable<ProductResponse>> { Message = "OK.", Data = productsDto };
            }

            products = await _productRepository.GetAllPagedAsync(request.PageNumber, request.PageSize);
            productsDto = ProductMapper.Mapper.Map<List<ProductResponse>>(products);
            return new PagedResponse<IEnumerable<ProductResponse>>(productsDto, request.PageNumber, request.PageSize, "OK");
        }
    }
}
