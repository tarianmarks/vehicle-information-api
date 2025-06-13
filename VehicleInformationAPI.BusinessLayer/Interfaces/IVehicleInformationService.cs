using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.BusinessLayer.Interfaces
{
    public interface IVehicleInformationService
    {
        public Task<Models.VehicleInformation> StoreVehicleInDataStore(VehicleInformation vehicle);

        public Task<Models.VehicleInformation> GetVehicleInformationByVin(string vin);
        public Task<List<Models.VehicleInformation>> GetListOfVehicleInformation(PaginationFilterRequest request);
        public Task<List<VehicleInformationExtended>> GetExtendedVehicleInformation();
        public Task<bool> PopulateVehicleInformation(string csvFile);
    }
}
