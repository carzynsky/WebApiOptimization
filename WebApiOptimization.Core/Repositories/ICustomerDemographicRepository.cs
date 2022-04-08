﻿using System.Collections.Generic;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface ICustomerDemographicRepository : IRepository<CustomerDemographic>
    {
        CustomerDemographic GetByCustomerTypeId(string customerTypeId);
    }
}
