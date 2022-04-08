using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOptimization.Core.Entities
{
    /// <summary>
    /// Not used anyway
    /// </summary>
    ///
    [Table("CustomerCustomerDemo")]
    public class CustomerCustomerDemo
    {
        [Key]
        public int CustomerID { get; set; }

        public int CustomerTypeID { get; set; }
    }
}
