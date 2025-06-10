namespace VehicleInformationAPI.DataLayer.Models
{
    public class VehicleInformationDataObject
    {
        public int DealerId { get; set; }
        public string? VIN { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
