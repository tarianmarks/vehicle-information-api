using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using VehicleInformationAPI.BusinessLayer;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.Controllers;
using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.UnitTests.Controllers
{
    public class VehicleInformationControllerTest
    {
        private Mock<ILogger<VehicleInformationController>> _mockLogger;
        private Mock<IVehicleInformationService> _mockBL;
        private VehicleInformationController _controller;

        private VehicleInformation _mockVehicleInformation = new VehicleInformation()
        {
            DealerId = "12345",
            Vin = "14LAKDF2Q3231",
            ModifiedDate = DateTime.Now
        };
    
        public VehicleInformationControllerTest() {
            _mockLogger = new Mock<ILogger<VehicleInformationController>>();
            _mockBL = new Mock<IVehicleInformationService>();
            _controller = new VehicleInformationController(_mockBL.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetGetVehicleInformationByVINTest_Should_Return_200()
        {
            _mockBL.Setup(bl => bl.GetVehicleInformationByVin(It.IsAny<string>())).Returns(Task.FromResult(_mockVehicleInformation));

            //Act
            var result = await _controller.GetVehicleInformationByVin(_mockVehicleInformation.Vin!);
            var resultType = result as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(200, resultType!.StatusCode);
        }

        [Fact]
        public async Task GetVehicleInformationByVIN_Should_Return_400_OnFail()
        {
            //Act
            var result = await _controller.GetVehicleInformationByVin(string.Empty);
            var resultType = result as BadRequestResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(400, resultType!.StatusCode);
        }
    }
}
