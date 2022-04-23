using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.ProductCommands;
using WebApiOptimization.Application.Helpers;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.ProductHandlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ResponseBuilder<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResponseBuilder<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = ProductMapper.Mapper.Map<Product>(request);
            if(productEntity == null)
            {
                return new ResponseBuilder<ProductResponse> { Message = ResponseBuilderHelper.InvalidData, Data = null };
            }

            try
            {
                var newProduct = await _productRepository.AddAsync(productEntity);
                var response = ProductMapper.Mapper.Map<ProductResponse>(newProduct);
                return new ResponseBuilder<ProductResponse> { Message = "Product created.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<ProductResponse> { Message = $"Product not created! Error: {e.InnerException.Message}", Data = null };

            }
        }
    }
}
