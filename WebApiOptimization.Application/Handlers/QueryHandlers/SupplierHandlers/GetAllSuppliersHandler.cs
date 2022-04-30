using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.SupplierQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Application.Wrappers;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.SupplierHandlers
{
    public class GetAllSuppliersHandler : IRequestHandler<GetAllSuppliersQuery, ResponseBuilder<IEnumerable<SupplierResponse>>>
    {
        private readonly ISupplierRepository _supplierRepository;

        public GetAllSuppliersHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<ResponseBuilder<IEnumerable<SupplierResponse>>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            List<Supplier> suppliers;
            List<SupplierResponse> suppliersDto;

            if(request.PageNumber == 0 || request.PageSize == 0)
            {
                suppliers = await _supplierRepository.GetAllAsync();
                suppliersDto = SupplierMapper.Mapper.Map<List<SupplierResponse>>(suppliers);
                return new ResponseBuilder<IEnumerable<SupplierResponse>> { Message = "OK.", Data = suppliersDto };
            }

            suppliers = await _supplierRepository.GetAllPagedAsync(request.PageNumber, request.PageSize);
            suppliersDto = SupplierMapper.Mapper.Map<List<SupplierResponse>>(suppliers);
            return new PagedResponse<IEnumerable<SupplierResponse>>(suppliersDto, request.PageNumber, request.PageSize, "OK");
        }
    }
}
