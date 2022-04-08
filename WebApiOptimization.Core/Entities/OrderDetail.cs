using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOptimization.Core.Entities
{
    [Table("Order Details")]
    public class OrderDetail
    {
        [Key]
        public int OrderID { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

        [Key]
        public int ProductID { get; set; }

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
