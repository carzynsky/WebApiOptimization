﻿using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;
using WebApiOptimization.Infrastructure.Data;
using WebApiOptimization.Infrastructure.Repositories.Base;

namespace WebApiOptimization.Infrastructure.Repositories
{
    public class CustomerDemographicRepository : Repository<CustomerDemographic>, ICustomerDemographicRepository
    {
        public CustomerDemographicRepository(NorthwndContext northwndContext) : base(northwndContext)
        {

        }
    }
}
