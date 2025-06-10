namespace VehicleInformationAPI.DataLayer.Models
{
    public class VehicleInformation
    {
        public int DealerId { get; set; }
        public string? VIN { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
