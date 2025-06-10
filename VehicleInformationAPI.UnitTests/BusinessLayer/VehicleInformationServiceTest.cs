using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using VehicleInformationAPI.BusinessLayer;
using VehicleInformationAPI.BusinessLayer.BusinessObjects;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.DataLayer.Repositories;
using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.UnitTests.Controllers
{
    public class VehicleInformationServiceTest
    {
        private readonly Mock<ILogger<VehicleInformationService>> _mockBlLogger;
        private readonly IMapper _mockMapper;
        private Mock<IVehicleInformationRepository> _mockRepository;
        private VehicleInformationService _service;

        private Models.VehicleInformation _mockVehicleInformation = new Models.VehicleInformation()
        {
            DealerId = 12345,
            VIN = "14LAKDF2Q3231",
            ModifiedDate = DateTime.Now
        };
        
        private List<DataLayer.Models.VehicleInformation> _mockRepositoryVehicleInformation = new List<DataLayer.Models.VehicleInformation>(){
            new DataLayer.Models.VehicleInformation()
            {
                DealerId = 12345,
                VIN = "14LAKDF2Q3231",
                ModifiedDate = DateTime.Now
            }
        };
    
        public VehicleInformationServiceTest() {
            _mockBlLogger = new Mock<ILogger<VehicleInformationService>>();
            _mockRepository = new Mock<IVehicleInformationRepository>();
            var config = new MapperConfiguration(cfg =>
            {               
                cfg.CreateMap<Models.VehicleInformation, DataLayer.Models.VehicleInformation>();
                cfg.CreateMap<DataLayer.Models.VehicleInformation, Models.VehicleInformation>();
            });
            _mockMapper = config.CreateMapper();
            _service = new VehicleInformationService(_mockRepository.Object, _mockMapper!, _mockBlLogger.Object);
        }

        [Fact]
        public async Task GetGetVehicleInformationByVINTest_Should_Return_Results()
        {
            _mockRepository.Setup(repo => repo.GetAllVehicles()).Returns(Task.FromResult(_mockRepositoryVehicleInformation));

            //Act
            var result = await _service.GetVehicleInformationByVIN(_mockVehicleInformation.VIN!);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.VIN, _mockVehicleInformation.VIN);
        }

        [Fact]
        public async Task GetListOfVehicleInformation_Should_Paginate()
        {
            var request = new PaginationFilterRequest { PageNumber = 1 , PageSize = 5 };

            _mockRepository.Setup(repo => repo.GetAllVehicles()).Returns(Task.FromResult(_mockRepositoryVehicleInformation));

            //Act
            var result = await _service.GetListOfVehicleInformation(request);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Count > 0);
        }
        
        [Fact]
        public async Task GetListOfVehicleInformation_Should_FilterByModifiedDate()
        {
            var request = new PaginationFilterRequest { ModifiedDate = DateTime.Now };

            _mockRepository.Setup(repo => repo.GetAllVehicles()).Returns(Task.FromResult(_mockRepositoryVehicleInformation));

            //Act
            var result = await _service.GetListOfVehicleInformation(request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result[0].ModifiedDate, DateTime.Now);
        }
        
        [Fact]
        public async Task GetListOfVehicleInformation_Should_FilterByDealerId()
        {
            var request = new PaginationFilterRequest { DealerId = 5 };

            _mockRepository.Setup(repo => repo.GetAllVehicles()).Returns(Task.FromResult(_mockRepositoryVehicleInformation));

            //Act
            var result = await _service.GetListOfVehicleInformation(request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result[0].DealerId, request.DealerId);
        }
    }
}
