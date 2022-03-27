using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOptimization.Core.Entities
{
    [Table("EmployeeTerritories")]
    public class EmployeeTerritory
    {
        [Key]
        public int EmployeeID { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }

        [Key]
        [StringLength(20)]
        [Required]
        public string TerritoryID { get; set; }

        [ForeignKey("TerritoryID")]
        public virtual Territory Territory { get; set; }
    }
}
