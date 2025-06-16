using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleInformationAPI.Models
{
    public class VehicleInformationExtended : VehicleInformation
    {
        public string? Make { get; set; } = string.Empty;
        public string? Model { get; set; } = string.Empty;
        public string? ModelYear { get; set; } = string.Empty;
        public string? Trim { get; set; } = string.Empty;
    }
}
