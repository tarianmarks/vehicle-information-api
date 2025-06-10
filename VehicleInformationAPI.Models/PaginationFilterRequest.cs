namespace VehicleInformationAPI.Models
{
    public class PaginationFilterRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int DealerId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
