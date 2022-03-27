using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOptimization.Core.Entities
{
    [Table("Territories")]
    public class Territory
    {
        [Key]
        [Column("TerritoryID")]
        [Required]
        public string TerritoryId { get; set; }

        [StringLength(50)]
        [Required]
        public string TerritoryDescription { get; set; }

        public int RegionID { get; set; }

        [ForeignKey("RegionID")]
        public virtual Region Region { get; set; }

    }
}
