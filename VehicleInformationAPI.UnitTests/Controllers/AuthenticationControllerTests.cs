using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using VehicleInformationAPI.BusinessLayer;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.Controllers;
using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.UnitTests.Controllers
{
    public class AuthenticationControllerTests
    {
        private Mock<ILogger<AuthenticationController>> _mockLogger;
        private Mock<BusinessLayer.Interfaces.IAuthenticationService> _mockAuthentication;
        private AuthenticationController _controller;

        private VehicleInformation _mockVehicleInformation = new VehicleInformation()
        {
            DealerId = "12345",
            Vin = "14LAKDF2Q3231",
            ModifiedDate = DateTime.Now
        };
    
        public AuthenticationControllerTests() {
            _mockLogger = new Mock<ILogger<AuthenticationController>>();
            _mockAuthentication = new Mock<BusinessLayer.Interfaces.IAuthenticationService>();
            _controller = new AuthenticationController(_mockAuthentication.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAuthentication_Should_Return_BearerToken()
        {
            var bearer = "Bearer:  sdjfafhjewioaornfaewf";
            var clientSec = "fkls-elwrwe-flksre-32432893";

            _mockAuthentication.Setup(auth => auth.GetAuthentication(It.IsAny<string>())).Returns(Task.FromResult(bearer));

            //Act
            var result = await _controller.GetAuthentication(clientSec);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(bearer, result);
        }

        [Fact]
        public async Task GetAuthentication_Should_Return_400_OnFail()
        {
            _mockAuthentication.Setup(auth => auth.GetAuthentication(It.IsAny<string>())).Returns(Task.FromResult(string.Empty));

            //Act
            var result = await _controller.GetAuthentication(string.Empty);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("", result);
        }
    }
}
