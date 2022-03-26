using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOptimization.Core.Entities
{
    /// <summary>
    /// Not used anyway
    /// </summary>
    public class CustomerCustomerDemo
    {
        [Key]
        [Column("CustomerID")]
        public int CustomerId { get; set; }

        [Column("CustomerTypeID")]
        public int CustomerTypeId { get; set; }
    }
}
