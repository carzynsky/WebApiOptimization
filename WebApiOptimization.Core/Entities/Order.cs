﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOptimization.Core.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [Column("OrderID")]
        public int OrderId { get; set; }

        [Column("CustomerID")]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        [Column("EmployeeID")]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime RequiredDate { get; set; }

        public DateTime ShippedDate { get; set; }

        // ToDo
        // fk
        public int ShipVia { get; set; }

        [Column(TypeName = "money")]
        public decimal Freight { get; set; }

        [StringLength(40)]
        public string ShipName { get; set; }

        [StringLength(60)]
        public string ShipAddress { get; set; }

        [StringLength(15)]
        public string ShipCity { get; set; }

        [StringLength(15)]
        public string ShipRegion { get; set; }

        [StringLength(10)]
        public string ShipPostalCode { get; set; }

        [StringLength(15)]
        public string ShipCountry { get; set; }
    }
}
