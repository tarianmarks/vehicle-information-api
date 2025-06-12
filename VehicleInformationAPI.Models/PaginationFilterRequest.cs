namespace VehicleInformationAPI.Models
{
    public class PaginationFilterRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string DealerId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
