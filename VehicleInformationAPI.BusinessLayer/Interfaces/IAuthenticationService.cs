using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.BusinessLayer.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<string> GetAuthentication(string clientSec);
    }
}
