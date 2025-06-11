using VehicleInformationAPI.BusinessLayer.BusinessObjects;
using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.BusinessLayer.Interfaces
{
    public interface IReadFromCsv
    {
        public List<VehicleInformation> ReadFile(string csvFile);
        //public VehicleInformationBO ReadFile(string csvFile);
    }
}
