using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOptimization.Core.Entities
{
    [Table("EmployeeTerritories")]
    public class EmployeeTerritory
    {
        [Key]
        [Column("EmployeeID")]
        public int EmployeeId { get; set; }

        /*

        [ForeignKey("EmployeeID")]
        [NotMapped]
        public virtual Employee Employee { get; set; }
        */

        [StringLength(20)]
        [Required]
        [Column("TerritoryID")]
        public string TerritoryId { get; set; }

        /*

        [ForeignKey("TerritoryID")]
        public virtual Territory Territory { get; set; }
        */
    }
}
