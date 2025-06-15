using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInformationAPI.DataLayer;
using VehicleInformationAPI.DataLayer.Models;
using VehicleInformationAPI.DataLayer.Repositories;

namespace VehicleInformationAPI.UnitTests.DataLayer
{
    public class VehicleInformationRepositoryTests
    {
        private readonly Mock<ILogger<VehicleInformationRepository>> _mockLogger;
        private readonly DataContext _mockDataContext;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly DbContextOptions<DataContext> _mockOptions;
        private VehicleInformationRepository _repository;

        private VehicleInformation _mockRepositoryVehicleInformation =
            new VehicleInformation()
            {
                id = 1,
                dealer_Id = "12345",
                vin = "14LAKDF2Q3231",
                modified_date = DateTime.Now
            };

        private List<VehicleInformation> _mockRepositoryVehicles = new List<VehicleInformation>(){
            new VehicleInformation()
            {
                id = 1,
                dealer_Id = "12345",
                vin = "14LAKDF2Q3231",
                modified_date = DateTime.Now
            },
            new VehicleInformation()
            {
                id = 2,
                dealer_Id = "12345",
                vin = "14LAKDF2Q3231OIEWRA",
                modified_date = DateTime.Now
            }
        };

        private List<VehicleInformationExtended> _mockRepositoryExtendedVehicles = new List<VehicleInformationExtended>(){
            new VehicleInformationExtended()
            {
                id = 1,
                dealer_Id = "12345",
                vin = "14LAKDF2Q3231",
                modified_date = DateTime.Now,
                make = "Chevy",
                model = "Tahoe",
                year = "2020/08/11"
            },
            new VehicleInformationExtended()
            {
                id = 2,
                dealer_Id = "12345",
                vin = "14LAKDF2Q3231OIEWRA",
                modified_date = DateTime.Now,
                make = "GMC",
                model = "Yukon",
                year = "2020/08/11"
            }
        };

        public VehicleInformationRepositoryTests()
        {
            _mockLogger = new Mock<ILogger<VehicleInformationRepository>>();
            _mockConfiguration = new Mock<IConfiguration>();
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase(Guid.NewGuid().ToString());
            _mockDataContext = new DataContext(_mockConfiguration.Object, options.Options);

            _repository = new VehicleInformationRepository(_mockDataContext, _mockConfiguration.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetVehicleInformationByVinTest_Should_Return_Results()
        {
            //Act
            _mockDataContext.Add(_mockRepositoryVehicleInformation);
            await _mockDataContext.SaveChangesAsync();
            
            var result = await _repository.GetVehicleInformationByVin(_mockRepositoryVehicleInformation.vin!);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.vin, _mockRepositoryVehicleInformation.vin);
        }

        [Fact]
        public async Task GetAllVehicles_Should_Return_Values()
        {
            //Act
            _mockDataContext.Add(_mockRepositoryVehicles[0]);
            _mockDataContext.Add(_mockRepositoryVehicles[1]);
            await _mockDataContext.SaveChangesAsync();
            
            var result = await _repository.GetAllVehicles();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task StoreVehicleInformation_Should_Add_Vehicles()
        {
            var result = await _repository.StoreVehicleInformation(_mockRepositoryVehicles);

            Assert.True(result);
        }
        
        [Fact]
        public async Task StoreExtendedVehicleInformation_Should_Add_Vehicles()
        {
            var result = await _repository.StoreExtendedVehicleInformation(_mockRepositoryExtendedVehicles);

            Assert.True(result);
        }
    }
}
