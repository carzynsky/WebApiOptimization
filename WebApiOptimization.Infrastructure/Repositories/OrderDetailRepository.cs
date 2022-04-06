﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;
using WebApiOptimization.Infrastructure.Data;
using WebApiOptimization.Infrastructure.Repositories.Base;

namespace WebApiOptimization.Infrastructure.Repositories
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(NorthwndContext northwndContext) : base(northwndContext)
        {

        }

        public override IEnumerable<OrderDetail> GetAll()
        {
            return NorthwndContext.OrderDetails.Include(x => x.Order).Include(x => x.Product);
        }
    }
}
