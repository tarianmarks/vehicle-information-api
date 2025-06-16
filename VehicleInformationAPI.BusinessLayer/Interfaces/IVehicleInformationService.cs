using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.BusinessLayer.Interfaces
{
    public interface IVehicleInformationService
    {
        public Task<VehicleInformation> GetVehicleInformationByVin(string vin);
        public Task<List<VehicleInformation>> GetListOfVehicleInformation(PaginationFilterRequest request);
        public Task<List<VehicleInformationExtended>> GetExtendedVehicleInformation();
        public Task<List<VehicleInformationExtended>> PopulateVehicleInformation(string csvFile);
    }
}
