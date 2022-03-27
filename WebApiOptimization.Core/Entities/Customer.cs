using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOptimization.Core.Entities
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [StringLength(5)]
        [Column("CustomerID")]
        public string CustomerId { get; set; }

        [StringLength(40)]
        [Required]
        public string CompanyName { get; set; }

        [StringLength(30)]
        public string ContactName{ get; set; }

        [StringLength(30)]
        public string ContactTitle { get; set; }

        [StringLength(60)]
        public string Address { get; set; }

        [StringLength(15)]
        public string City { get; set; }

        [StringLength(15)]
        public string Region { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }
    }
}
