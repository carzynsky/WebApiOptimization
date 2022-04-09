using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.ProductCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.ProductHandlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, ResponseBuilder<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public DeleteProductHandler(IProductRepository productRepository, IOrderDetailRepository orderDetailRepository)
        {
            _productRepository = productRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<ResponseBuilder<ProductResponse>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productToDelete = _productRepository.GetById(request.Id);
            if(productToDelete == null)
            {
                return new ResponseBuilder<ProductResponse> { Message = $"Product with id={request.Id} not found!", Data = null };
            }

            // Find order details with this orderId
            var orderDetailsWithThisOrderId = _orderDetailRepository.GetByOrderId(request.Id).ToList();
            if (orderDetailsWithThisOrderId.Any())
            {
                _orderDetailRepository.DeleteRange(orderDetailsWithThisOrderId);
            }

            _productRepository.Delete(productToDelete);
            var response = ProductMapper.Mapper.Map<ProductResponse>(productToDelete);
            return new ResponseBuilder<ProductResponse> { Message = "Product deleted.", Data = response };
        }
    }
}
