using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOptimization.Core.Entities
{
    [Table("Territories")]
    public class Territory
    {
        [Key]
        [Column("TerritoryID")]
        public int TerritoryId { get; set; }

        [StringLength(50)]
        [Required]
        public string TerritoryDescription { get; set; }

        [Column("RegionID")]
        public int RegionId { get; set; }

        [ForeignKey("RegionID")]
        public virtual Region Region { get; set; }
    }
}
