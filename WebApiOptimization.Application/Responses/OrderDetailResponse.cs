﻿using WebApiOptimization.Core.Entities;

namespace WebApiOptimization.Application.Responses
{
    public class OrderDetailResponse
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
    }
}
