using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.ProductCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.ProductHandlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ResponseBuilder<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResponseBuilder<ProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productToUpdate = await _productRepository.GetByIdAsync(request.ProductId);
            if (productToUpdate == null)
            {
                return new ResponseBuilder<ProductResponse> { Message = $"Product with id={request.ProductId} not found!", Data = null };
            }

            try
            {
                var productToUpdateEntity = ProductMapper.Mapper.Map<Product>(request);
                _productRepository.Update(productToUpdateEntity);
                var response = ProductMapper.Mapper.Map<ProductResponse>(productToUpdateEntity);
                return new ResponseBuilder<ProductResponse> { Message = "Product updated.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<ProductResponse> { Message = $"Product not updated! Error: {e.InnerException.Message}", Data = null };
            }
        }
    }
}
