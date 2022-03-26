using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Supplier;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.SupplierHandlers
{
    public class DeleteSupplierHandler : IRequestHandler<DeleteSupplierCommand, SupplierResponse>
    {
        private readonly ISupplierRepository _supplierRepository;
        public DeleteSupplierHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public async Task<SupplierResponse> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplierToDeleteEntity = _supplierRepository.GetById(request.Id);
            if (supplierToDeleteEntity == null)
            {
                return null;
            }

            _supplierRepository.Delete(supplierToDeleteEntity);
            var response = SupplierMapper.Mapper.Map<SupplierResponse>(supplierToDeleteEntity);
            return response;
        }
    }
}
