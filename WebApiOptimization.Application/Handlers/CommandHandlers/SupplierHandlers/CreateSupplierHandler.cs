using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.SupplierCommands;
using WebApiOptimization.Application.Helpers;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.SupplierHandlers
{
    public class CreateSupplierHandler : IRequestHandler<CreateSupplierCommand, ResponseBuilder<SupplierResponse>>
    {
        private readonly ISupplierRepository _supplierRepository;

        public CreateSupplierHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<ResponseBuilder<SupplierResponse>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplierEntity = SupplierMapper.Mapper.Map<Supplier>(request);
            if (supplierEntity == null)
            {
                return new ResponseBuilder<SupplierResponse> { Message = ResponseBuilderHelper.InvalidData, Data = null };
            }

            try
            {
                var newSupplier = _supplierRepository.Add(supplierEntity);
                var response = SupplierMapper.Mapper.Map<SupplierResponse>(newSupplier);
                return new ResponseBuilder<SupplierResponse> { Message = "Supplier created.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<SupplierResponse> { Message = $"Supplier not created! Error: {e.Message}", Data = null };
            }
        }
    }
}
