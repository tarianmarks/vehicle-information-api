namespace VehicleInformationAPI.Models
{
    public class NhtsaResults
    {
        public int Count { get; set; }
        public string? Message { get; set; }
        public string SearchCriteria { get; set; } = string.Empty;
        public List<VehicleInformationExtended>? Results {  get; set; }
    }
    public class VehicleInformationExtended : VehicleInformation
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string ModelYear { get; set; }
        public string Trim {  get; set; }
    }
}
