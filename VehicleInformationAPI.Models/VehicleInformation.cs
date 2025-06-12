namespace VehicleInformationAPI.Models
{
    public class VehicleInformation
    {
        public string DealerId { get; set; } = string.Empty;
        public string? Vin { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
