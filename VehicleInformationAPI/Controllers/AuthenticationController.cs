using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.BusinessLayer;
using Microsoft.AspNetCore.Authorization;

namespace VehicleInformationAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService authenticationService, ILogger<AuthenticationController> logger) : Controller
    {
        private readonly ILogger<AuthenticationController>? _logger = logger;
        private readonly IAuthenticationService? _authenticationService = authenticationService;

        /// <summary>
        /// Gets a bearer token to provide access
        /// </summary>
        /// <param></param>
        /// <returns>Bearer token as a string</returns>
        [HttpGet("authentication/{clientSec}")]
            public async Task<string> GetAuthentication(string clientSec)
            {
                var result = await _authenticationService!.GetAuthentication(clientSec);

                return result;
            }
        }
}
