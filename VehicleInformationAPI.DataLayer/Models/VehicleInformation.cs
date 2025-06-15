using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleInformationAPI.DataLayer.Models
{
    [Table("vehicle_information")]
    public class VehicleInformation
    {
        public int id { get; set; }
        [Column("dealer_Id")]
        public string? dealer_Id { get; set; }
        [Column("vin")]
        public string vin { get; set; } = string.Empty;
        [Column("modified_date")]
        public DateTime? modified_date { get; set; }
        [Column("make")]
        public string? make { get; set; }
        [Column("model")]
        public string? model { get; set; }
        [Column("year")]
        public string? year { get; set; }
    }
    public class VehicleInformationExtended : VehicleInformation
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string ModelYear { get; set; }
        public string Trim { get; set; }
    }
}
