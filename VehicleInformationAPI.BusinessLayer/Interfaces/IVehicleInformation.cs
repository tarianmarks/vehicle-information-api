using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.BusinessLayer.Interfaces
{
    public interface IVehicleInformation
    {
        public Task<Models.VehicleInformation> StoreVehicleInDataStore(VehicleInformation vehicle);

        public Task<Models.VehicleInformation> GetVehicleInformationByVIN(string vin);
    }
}
