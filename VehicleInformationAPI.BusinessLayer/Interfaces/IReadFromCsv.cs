using VehicleInformationAPI.BusinessLayer.BusinessObjects;
using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.BusinessLayer.Interfaces
{
    public interface IReadFromCsv
    {
        public Task<List<VehicleInformation>> ReadFile(string csvFile);
    }
}
