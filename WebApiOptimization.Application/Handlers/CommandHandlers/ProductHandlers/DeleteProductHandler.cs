using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Product;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.ProductHandlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productToDelete = _productRepository.GetById(request.Id);
            if(productToDelete == null)
            {
                return null;
            }

            _productRepository.Delete(productToDelete);
            var response = ProductMapper.Mapper.Map<ProductResponse>(productToDelete);
            return response;
        }
    }
}
