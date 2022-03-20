﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOptimization.Core.Entities
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        [Column("OrderID")]
        public int OrderId { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

        [Column("ProductID")]
        public int ProductId { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
         
        [Column(TypeName = "money")]
        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public short Quantity { get; set; }

        [Required]
        public float Discount { get; set; }
    }
}
