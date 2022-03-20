using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOptimization.Core.Entities
{
    [Table("EmployeeTerritories")]
    public class EmployeeTerritory
    {
        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee Employee { get; set; }

        [StringLength(20)]
        [Required]
        public string TerritoryId { get; set; }

        [ForeignKey(nameof(TerritoryId))]
        public virtual Territory Territory { get; set; }
    }
}
