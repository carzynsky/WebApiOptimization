using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOptimization.Core.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [Column("ProductID")]
        public int ProductId { get; set; }

        [StringLength(40)]
        [Required]
        public string ProductName { get; set; }

        [Column("SupplierID")]
        public int SupplierId { get; set; }

        [ForeignKey("SupplierID")]
        public virtual Supplier Supplier { get; set; }

        [Column("CategoryID")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        [StringLength(40)]
        public string QuantityPerUnit { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public short UnitsInStock { get; set; }

        public short UnitsOnOrder { get; set; }

        public short ReorderLevel { get; set; }

        public bool Discontinued { get; set; }
    }
}
