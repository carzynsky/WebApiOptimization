using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOptimization.Core.Entities
{
    [Table("Region")]
    public class Region
    {
        [Key]
        [Column("RegionID")]
        public int RegionId { get; set; }

        [StringLength(50)]
        [Required]
        public string RegionDescription { get; set; }
    }
}
