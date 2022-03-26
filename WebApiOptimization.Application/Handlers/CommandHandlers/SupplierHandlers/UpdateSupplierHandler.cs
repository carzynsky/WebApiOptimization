using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Supplier;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.SupplierHandlers
{
    public class UpdateSupplierHandler : IRequestHandler<UpdateSupplierCommand, SupplierResponse>
    {
        private readonly ISupplierRepository _supplierRepository;
        public UpdateSupplierHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public async Task<SupplierResponse> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplierToUpdate = _supplierRepository.GetById(request.SupplierId);
            if (supplierToUpdate == null)
            {
                return null;
            }

            var supplierToUpdateEntity = SupplierMapper.Mapper.Map<Supplier>(request);
            if (supplierToUpdateEntity == null)
            {
                return null;
            }

            _supplierRepository.Update(supplierToUpdateEntity);
            var response = SupplierMapper.Mapper.Map<SupplierResponse>(supplierToUpdateEntity);
            return response;
        }
    }
}
