using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Product;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.ProductHandlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = ProductMapper.Mapper.Map<Product>(request);
            if(productEntity == null)
            {
                return null;
            }

            var newProduct = _productRepository.Add(productEntity);
            var response = ProductMapper.Mapper.Map<ProductResponse>(newProduct);
            return response;
        }
    }
}
