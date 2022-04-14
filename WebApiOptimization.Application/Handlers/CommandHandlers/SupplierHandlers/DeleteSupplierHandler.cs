using MediatR;
using System;
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
            var supplierToDeleteEntity = await _supplierRepository.GetByIdAsync(request.Id);
            if (supplierToDeleteEntity == null)
            {
                return new ResponseBuilder<SupplierResponse> { Message = $"Supplier with id={request.Id} not found!", Data = null };
            }

            try
            {
                // Find products with this supplierId
                var productsWithThisSupplierId = await _productRepository.GetBySupplierIdAsync(request.Id);
                if (productsWithThisSupplierId.Any())
                {
                    productsWithThisSupplierId.ForEach(x => x.SupplierID = null);
                    _productRepository.UpdateRange(productsWithThisSupplierId);
                }

                _supplierRepository.Delete(supplierToDeleteEntity);
                var response = SupplierMapper.Mapper.Map<SupplierResponse>(supplierToDeleteEntity);
                return new ResponseBuilder<SupplierResponse> { Message = "Supplier deleted.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<SupplierResponse> { Message = $"Supplier not deleted! Error: {e.InnerException.Message}", Data = null };
            }
        }
    }
}
