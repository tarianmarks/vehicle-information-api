using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using VehicleInformationAPI.BusinessLayer;
using VehicleInformationAPI.BusinessLayer.BusinessObjects;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.DataLayer.Models;
using VehicleInformationAPI.DataLayer.Interfaces;
using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.UnitTests.Controllers
{
    public class VehicleInformationServiceTest
    {
        private readonly Mock<ILogger<VehicleInformationService>> _mockBlLogger;
        private readonly IMapper _mockMapper;
        private Mock<IVehicleInformationRepository> _mockRepository;
        private VehicleInformationService _service;

        private VehicleInformation _mockVehicleInformation = new VehicleInformation()
        {
            DealerId = 12345,
            VIN = "14LAKDF2Q3231",
            ModifiedDate = DateTime.Now
        };
        
        private List<VehicleInformation> _mockRepositoryVehicleInformation = new List<VehicleInformation>(){
            new VehicleInformation()
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
                cfg.CreateMap<VehicleInformation, VehicleInformationDataObject>();
                cfg.CreateMap<VehicleInformationDataObject, VehicleInformation>();
            });
            _mockMapper = config.CreateMapper();
            _service = new VehicleInformationService(_mockRepository.Object, _mockMapper!, _mockBlLogger.Object);
        }

        [Fact]
        public async Task GetVehicleInformationByVINTest_Should_Return_Results()
        {
            _mockRepository.Setup(repo => repo.GetVehicleInformationByVIN(It.IsAny<string>())).Returns(Task.FromResult(_mockVehicleInformation));

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
        
        [Theory]
        [InlineData(-1, 5)]
        [InlineData(5, -1)]
        public async Task GetListOfVehicleInformation_Should_ThrowException_For_Bad_Pagination(int pageSize, int pageNumber)
        {
            var request = new PaginationFilterRequest { PageNumber = pageNumber , PageSize = pageSize };

            _mockRepository.Setup(repo => repo.GetAllVehicles()).Returns(Task.FromResult(_mockRepositoryVehicleInformation));
            
            Assert.ThrowsAsync<ArgumentException>(() => _service.GetListOfVehicleInformation(request));
        }
        
        [Fact]
        public async Task GetListOfVehicleInformation_Should_FilterByModifiedDate()
        {
            var request = new PaginationFilterRequest { PageNumber = 0, PageSize = 0, ModifiedDate = DateTime.Parse("10/22/2022 2:30:15 PM") };

            _mockRepository.Setup(repo => repo.GetAllVehicles()).Returns(Task.FromResult(_mockRepositoryVehicleInformation));

            //Act
            var result = await _service.GetListOfVehicleInformation(request);

            //Assert
            Assert.NotNull(result);
        }
        
        [Fact]
        public async Task GetListOfVehicleInformation_Should_FilterByDealerId()
        {
            var request = new PaginationFilterRequest { PageNumber = 0, PageSize = 0, DealerId = 12345 };

            _mockRepository.Setup(repo => repo.GetAllVehicles()).Returns(Task.FromResult(_mockRepositoryVehicleInformation));

            //Act
            var result = await _service.GetListOfVehicleInformation(request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result[0].DealerId, request.DealerId);
        }
    }
}
