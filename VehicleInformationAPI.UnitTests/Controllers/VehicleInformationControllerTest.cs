using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using VehicleInformationAPI.BusinessLayer;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.Controllers;

namespace VehicleInformationAPI.UnitTests.Controllers
{
    public class VehicleInformationControllerTest
    {
        private readonly Mock<ILogger<VehicleInformation>> _mockBlLogger;
        private Mock<ILogger<VehicleInformationController>> _mockLogger;
        private Mock<IVehicleInformation> _mockBL;
        private VehicleInformationController _controller;

        private Models.VehicleInformation _mockVehicleInformation = new Models.VehicleInformation()
        {
            DealerId = 12345,
            VIN = "14LAKDF2Q3231",
            ModifiedDate = DateTime.Now
        };
    
        public VehicleInformationControllerTest() {
            _mockBlLogger = new Mock<ILogger<VehicleInformation>>();
            _mockLogger = new Mock<ILogger<VehicleInformationController>>();
            _mockBL = new Mock<IVehicleInformation>(/*_mockBlLogger*/);
            _controller = new VehicleInformationController(_mockBL.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetGetVehicleInformationByVINTest_Should_Return_200()
        {
            _mockBL.Setup(bl => bl.GetVehicleInformationByVIN(It.IsAny<string>())).Returns(Task.FromResult(_mockVehicleInformation));

            //Act
            var result = await _controller.GetVehicleInformationByVIN(_mockVehicleInformation.VIN!);
            var resultType = result as OkResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(200, resultType!.StatusCode);
        }

        [Fact]
        public async Task GetVehicleInformationByVIN_Should_Return_400_OnFail()
        {
            //Act
            var result = await _controller.GetVehicleInformationByVIN(String.Empty);
            var resultType = result as BadRequestResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(400, resultType!.StatusCode);
        }
    }
}
