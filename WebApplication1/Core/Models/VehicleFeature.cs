using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Core.Models
{
    [Table("VehicleFeatures")]
    public class VehicleFeature
    {
        public int VehicleId { get; set; }
        public int FeatureId { get; set; }

        public Vehicle Vehilce { get; set; }
        public Feature Feature { get; set; }

    }

}