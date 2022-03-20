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
        [Column("CustomerTypeID")]
        public int CustomerTypeId { get; set; }

        [Column(TypeName = "ntext")]
        public string CustomerDesc { get; set; }
    }
}
