using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleInformationAPI.DataLayer.Models
{
    [Table("vehicle_information")]
    public class VehicleInformation
    {
        public int Id { get; set; } // Id (Primary key)
        [Column("dealer_Id")]
        public string DealerId { get; set; } // dealer_Id
        [Column("vin")]
        public string Vin { get; set; } // vin
        [Column("modified_date")]
        public DateTime? ModifiedDate { get; set; } // modified_date
        [Column("make")]
        public string Make { get; set; } // make (length: 50)
        [Column("model")]
        public string Model { get; set; } // model (length: 50)
        [Column("year")]
        public string Year { get; set; } // year (length: 50)
    }
}
