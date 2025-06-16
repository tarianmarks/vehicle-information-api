namespace VehicleInformationAPI.Models
{
    public class NhtsaResults
    {
        public int Count { get; set; }
        public string? Message { get; set; }
        public string SearchCriteria { get; set; } = string.Empty;
        public List<VehicleInformationExtended>? Results {  get; set; }
    }
}
