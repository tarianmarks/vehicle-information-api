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
    }
}
