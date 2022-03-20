using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOptimization.Core.Entities
{
    [Table("Shippers")]
    public class Shipper
    {
        [Key]
        [Column("ShipperID")]
        public int ShipperId { get; set; }

        [StringLength(40)]
        [Required]
        public string CompanyName { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }
    }
}
