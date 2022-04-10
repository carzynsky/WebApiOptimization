using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.ProductQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Product
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ResponseBuilder<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResponseBuilder<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var productEntity = _productRepository.GetById(request.Id, true);
            if(productEntity == null)
            {
                return new ResponseBuilder<ProductResponse> { Message = $"Product with id={request.Id} not found!", Data = null };
            }

            var response = ProductMapper.Mapper.Map<ProductResponse>(productEntity);
            return new ResponseBuilder<ProductResponse> { Message = "OK.", Data = response };
        }
    }
}
