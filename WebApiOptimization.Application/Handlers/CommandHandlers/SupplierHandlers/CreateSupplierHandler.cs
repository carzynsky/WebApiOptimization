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
    public class CreateSupplierHandler : IRequestHandler<CreateSupplierCommand, SupplierResponse>
    {
        private readonly ISupplierRepository _supplierRepository;
        public CreateSupplierHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public async Task<SupplierResponse> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplierEntity = SupplierMapper.Mapper.Map<Supplier>(request);
            if (supplierEntity == null)
            {
                return null;
            }

            var newSupplier = _supplierRepository.Add(supplierEntity);
            var reponse = SupplierMapper.Mapper.Map<SupplierResponse>(newSupplier);
            return reponse;
        }
    }
}
