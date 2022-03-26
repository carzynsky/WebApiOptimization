using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.Supplier;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Supplier
{
    public class GetSupplierByIdHandler : IRequestHandler<GetSupplierByIdQuery, SupplierResponse>
    {
        private readonly ISupplierRepository _supplierRepository;
        public GetSupplierByIdHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public async Task<SupplierResponse> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var supplierEntity = _supplierRepository.GetById(request.Id);
            var response = SupplierMapper.Mapper.Map<SupplierResponse>(supplierEntity);
            return response;
        }
    }
}
