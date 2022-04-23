using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.ProductQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Product
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
            var products = await _productRepository.GetAllAsync();
            var response = ProductMapper.Mapper.Map<IEnumerable<ProductResponse>>(products);
            return new ResponseBuilder<IEnumerable<ProductResponse>> { Message = "OK.", Data = response };
        }
    }
}
