﻿using System.Collections.Generic;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface ITerritoryRepository : IRepository<Territory>
    {
        IEnumerable<Territory> GetByRegionId(int regionId);
    }
}
