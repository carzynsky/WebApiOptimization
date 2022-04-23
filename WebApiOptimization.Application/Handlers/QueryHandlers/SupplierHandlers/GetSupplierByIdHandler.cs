using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.SupplierQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Supplier
{
    public class GetSupplierByIdHandler : IRequestHandler<GetSupplierByIdQuery, ResponseBuilder<SupplierResponse>>
    {
        private readonly ISupplierRepository _supplierRepository;

        public GetSupplierByIdHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<ResponseBuilder<SupplierResponse>> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var supplierEntity = await _supplierRepository.GetByIdAsync(request.Id);
            if(supplierEntity == null)
            {
                return new ResponseBuilder<SupplierResponse> { Message = $"Supplier with id={request.Id} not found!", Data = null };
            }

            var response = SupplierMapper.Mapper.Map<SupplierResponse>(supplierEntity);
            return new ResponseBuilder<SupplierResponse> { Message = "OK.", Data = response };
        }
    }
}
