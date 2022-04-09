using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.SupplierCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.SupplierHandlers
{
    public class DeleteSupplierHandler : IRequestHandler<DeleteSupplierCommand, ResponseBuilder<SupplierResponse>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IProductRepository _productRepository;

        public DeleteSupplierHandler(ISupplierRepository supplierRepository, IProductRepository productRepository)
        {
            _supplierRepository = supplierRepository;
            _productRepository = productRepository;
        }

        public async Task<ResponseBuilder<SupplierResponse>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplierToDeleteEntity = _supplierRepository.GetById(request.Id);
            if (supplierToDeleteEntity == null)
            {
                return new ResponseBuilder<SupplierResponse> { Message = $"Supplier with id={request.Id} not found!", Data = null };
            }

            // Find products with this supplierId
            var productsWithThisSupplierId = _productRepository.GetBySupplierId(request.Id).ToList();
            if (productsWithThisSupplierId.Any())
            {
                productsWithThisSupplierId.ForEach(x => x.SupplierID = null);
                _productRepository.UpdateRange(productsWithThisSupplierId);
            }

            _supplierRepository.Delete(supplierToDeleteEntity);
            var response = SupplierMapper.Mapper.Map<SupplierResponse>(supplierToDeleteEntity);
            return new ResponseBuilder<SupplierResponse> { Message = $"Supplier deleted.", Data = response };
        }
    }
}
