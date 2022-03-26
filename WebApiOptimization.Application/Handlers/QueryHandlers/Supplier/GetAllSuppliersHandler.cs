using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.Supplier;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Supplier
{
    public class GetAllSuppliersHandler : IRequestHandler<GetAllSuppliersQuery, IEnumerable<SupplierResponse>>
    {
        private readonly ISupplierRepository _supplierRepository;
        public GetAllSuppliersHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public async Task<IEnumerable<SupplierResponse>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            var suppliers = _supplierRepository.GetAll();
            var response = SupplierMapper.Mapper.Map<IEnumerable<SupplierResponse>>(suppliers);
            return response;
        }
    }
}
