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

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        [Column("CustomerTypeID")]
        public int CustomerTypeId { get; set; }

        [ForeignKey("CustomerTypeID")]
        public virtual CustomerDemographic CustomerDemographic { get; set; }
    }
}
