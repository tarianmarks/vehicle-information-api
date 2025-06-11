using CsvHelper.Configuration.Attributes;

namespace VehicleInformationAPI.BusinessLayer.BusinessObjects
{
    public class VehicleInformationBO
    {
        [Index(0)]
        public int DealerId { get; set; }
        [Index(1)] 
        public string? VIN { get; set; }
        [Index(2)] 
        public DateTime ModifiedDate { get; set; }
    }
}
