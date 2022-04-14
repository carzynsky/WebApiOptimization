using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.SupplierCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.SupplierHandlers
{
    public class UpdateSupplierHandler : IRequestHandler<UpdateSupplierCommand, ResponseBuilder<SupplierResponse>>
    {
        private readonly ISupplierRepository _supplierRepository;

        public UpdateSupplierHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<ResponseBuilder<SupplierResponse>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplierToUpdate = await _supplierRepository.GetByIdAsync(request.SupplierId);
            if (supplierToUpdate == null)
            {
                return new ResponseBuilder<SupplierResponse> { Message = $"Supplier with id={request.SupplierId} not found!", Data = null };
            }

            try
            {
                var supplierToUpdateEntity = SupplierMapper.Mapper.Map<Supplier>(request);
                _supplierRepository.Update(supplierToUpdateEntity);
                var response = SupplierMapper.Mapper.Map<SupplierResponse>(supplierToUpdateEntity);
                return new ResponseBuilder<SupplierResponse> { Message = "Supplier updated.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<SupplierResponse> { Message = $"Supplier not updated! Error: {e.InnerException.Message}", Data = null };
            }
        }
    }
}
