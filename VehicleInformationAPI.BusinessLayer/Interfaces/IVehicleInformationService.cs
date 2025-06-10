using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.BusinessLayer.Interfaces
{
    public interface IVehicleInformationService
    {
        public Task<Models.VehicleInformation> StoreVehicleInDataStore(VehicleInformation vehicle);

        public Task<Models.VehicleInformation> GetVehicleInformationByVIN(string vin);
        public Task<List<Models.VehicleInformation>> GetListOfVehicleInformation(PaginationFilterRequest request);
    }
}
