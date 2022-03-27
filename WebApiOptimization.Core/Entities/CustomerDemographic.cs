using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOptimization.Core.Entities
{
    /// <summary>
    /// Not used anyway
    /// </summary>
    [Table("CustomerDemographics")]
    public class CustomerDemographic
    {
        [Key]
        [StringLength(10)]
        [Column("CustomerTypeID")]
        public string CustomerTypeId { get; set; }

        [Column(TypeName = "ntext")]
        public string CustomerDesc { get; set; }
    }
}
