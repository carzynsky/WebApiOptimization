﻿using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.CustomerCustomerDemoQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.CustomerCustomerDemoHandlers
{
    public class GetCustomerCustomerDemoHandler : IRequestHandler<GetCustomerCustomerDemoQuery, ResponseBuilder<IEnumerable<CustomerCustomerDemoResponse>>>
    {
        private readonly ICustomerCustomerDemoRepository _customerCustomerDemoRepository;

        public GetCustomerCustomerDemoHandler(ICustomerCustomerDemoRepository customerCustomerDemoRepository)
        {
            _customerCustomerDemoRepository = customerCustomerDemoRepository;
        }
        public async Task<ResponseBuilder<IEnumerable<CustomerCustomerDemoResponse>>> Handle(GetCustomerCustomerDemoQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<CustomerCustomerDemo> customerCustomerDemos;

            if(request.CustomerId != null && request.CustomerTypeId == null)
            {
                customerCustomerDemos = _customerCustomerDemoRepository.GetByCustomerId(request.CustomerId, true);
            }
            else if(request.CustomerId == null && request.CustomerTypeId != null)
            {
                customerCustomerDemos = _customerCustomerDemoRepository.GetByCustomerTypeId(request.CustomerTypeId, true);
            }
            else if(request.CustomerId != null && request.CustomerTypeId != null)
            {
                customerCustomerDemos = _customerCustomerDemoRepository.GetByCustomerIdAndCustomerTypeId(request.CustomerId, request.CustomerTypeId, true);
            }
            else
            {
                customerCustomerDemos = _customerCustomerDemoRepository.GetAll();
            }

            var response = CustomerCustomerDemoMapper.Mapper.Map<IEnumerable<CustomerCustomerDemoResponse>>(customerCustomerDemos);
            return new ResponseBuilder<IEnumerable<CustomerCustomerDemoResponse>> { Message = "OK.", Data = response };
        }
    }
}
