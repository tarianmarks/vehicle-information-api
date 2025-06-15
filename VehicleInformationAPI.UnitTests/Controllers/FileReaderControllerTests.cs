using Azure.Core;
using CsvHelper;
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
    public class FileReaderControllerTests
    {
        private Mock<ILogger<FileReaderController>> _mockLogger;
        private Mock<IReadFromCsv> _mockCsvReader;
        private FileReaderController _controller;

        private VehicleInformation _mockVehicleInformation = new VehicleInformation()
        {
            DealerId = "12345",
            Vin = "14LAKDF2Q3231",
            ModifiedDate = DateTime.Now
        };
    
        public FileReaderControllerTests() {
            _mockLogger = new Mock<ILogger<FileReaderController>>();
            _mockCsvReader = new Mock<IReadFromCsv>();
            _controller = new FileReaderController(_mockCsvReader.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Get_Should_Return_A_List_Of_Vehicles()
        {
            var vehicles = new List<VehicleInformation>(
            new List<VehicleInformation>() {
                new VehicleInformation()
                {
                    DealerId = "12345",
                    Vin = "14LAKDF2Q3231",
                    ModifiedDate = DateTime.Now
                },
                new VehicleInformation()
                {
                    DealerId = "12345",
                    Vin = "14LAKDF2Q3231OIEWRA",
                    ModifiedDate = DateTime.Now
                }
            });

            var csvFile = "C:\\Temp\\vehicleInfo.csv";

            _mockCsvReader.Setup(csv => csv.ReadFile(It.IsAny<string>())).Returns(Task.FromResult(vehicles));

            //Act
            var result = await _controller.Get(csvFile);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(vehicles, result);
        }
        
        [Fact]
        public async Task Get_Should_Throw_An_Exception()
        {
            _mockCsvReader.Setup(csv => csv.ReadFile(It.IsAny<string>())).Returns(Task.FromResult(new List<VehicleInformation>()));
                        
            //Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _controller.Get(string.Empty));
        }
    }
}
