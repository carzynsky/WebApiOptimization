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
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productToUpdate = _productRepository.GetById(request.ProductId);
            if (productToUpdate == null)
            {
                return null;
            }

            var productToUpdateEntity = ProductMapper.Mapper.Map<Product>(request);
            if (productToUpdateEntity == null)
            {
                return null;
            }

            _productRepository.Update(productToUpdateEntity);
            var response = ProductMapper.Mapper.Map<ProductResponse>(productToUpdateEntity);
            return response;
        }
    }
}
