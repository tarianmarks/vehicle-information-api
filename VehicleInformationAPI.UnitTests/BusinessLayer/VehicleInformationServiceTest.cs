using Microsoft.Extensions.Logging;
using Moq;
using VehicleInformationAPI.BusinessLayer;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.DataLayer.Interfaces;
using dataLayer = VehicleInformationAPI.DataLayer.Models;
using VehicleInformationAPI.Models;
using CsvHelper;
using VehicleInformationAPI.DataLayer.Repositories;
using Azure.Core;

namespace VehicleInformationAPI.UnitTests.Controllers
{
    public class VehicleInformationServiceTest
    {
        private readonly Mock<ILogger<VehicleInformationService>> _mockBlLogger;
        private readonly Mock<IMyMapper> _mockMyMapper;
        private Mock<IVehicleInformationRepository> _mockRepository;
        private VehicleInformationService _service;
        private readonly Mock<IReadFromCsv> _mockCsvReader;
        private readonly Mock<HttpClient> _mockHttpClient;

        private Models.VehicleInformation _mockVehicleInformation = new Models.VehicleInformation()
        {
            DealerId = "12345",
            Vin = "14LAKDF2Q3231",
            ModifiedDate = DateTime.Now
        };
        
        private dataLayer.VehicleInformation _mockRepositoryVehicleInformation = 
            new dataLayer.VehicleInformation()
            {
                dealer_Id = "12345",
                vin = "14LAKDF2Q3231",
                modified_date = DateTime.Now
            };
        
        private List<dataLayer.VehicleInformation> _mockRepositoryVehicles = new List<dataLayer.VehicleInformation>(){
            new dataLayer.VehicleInformation()
            {
                dealer_Id = "12345",
                vin = "14LAKDF2Q3231",
                modified_date = DateTime.Now
            },
            new dataLayer.VehicleInformation()
            {
                dealer_Id = "12345",
                vin = "14LAKDF2Q3231OIEWRA",
                modified_date = DateTime.Now
            }
        };
        
        private List<VehicleInformation> _mockVehicles = new List<VehicleInformation>(){
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
        };
    
        public VehicleInformationServiceTest() {
            _mockBlLogger = new Mock<ILogger<VehicleInformationService>>();
            _mockRepository = new Mock<IVehicleInformationRepository>();
            _mockCsvReader = new Mock<IReadFromCsv>();
            _mockHttpClient = new Mock<HttpClient>();
            _mockMyMapper = new Mock<IMyMapper>();

            _service = new VehicleInformationService(_mockRepository.Object, _mockMyMapper.Object, _mockCsvReader.Object, _mockBlLogger.Object, _mockHttpClient.Object);            
        }

        [Fact]
        public async Task GetVehicleInformationByVinTest_Should_Return_Results()
        {
            _mockMyMapper.Setup(x => x.MapVehicle(It.IsAny<dataLayer.VehicleInformation>())).Returns(_mockVehicleInformation);
            _mockRepository.Setup(repo => repo.GetVehicleInformationByVin(It.IsAny<string>())).Returns(Task.FromResult(_mockRepositoryVehicleInformation));

            //Act
            var result = await _service.GetVehicleInformationByVin(_mockVehicleInformation.Vin!);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Vin, _mockVehicleInformation.Vin);
        }

        [Fact]
        public async Task GetListOfVehicleInformation_Should_Paginate()
        {
            var request = new PaginationFilterRequest { PageNumber = 1 , PageSize = 5 };
            _mockMyMapper.Setup(x => x.MapVehicles(It.IsAny<List<dataLayer.VehicleInformation>>())).Returns(_mockVehicles);

            _mockRepository.Setup(repo => repo.GetAllVehicles()).Returns(Task.FromResult(_mockRepositoryVehicles));

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

            _mockRepository.Setup(repo => repo.GetAllVehicles()).Returns(Task.FromResult(_mockRepositoryVehicles));

            await Assert.ThrowsAsync<NullReferenceException>(async () => await _service.GetListOfVehicleInformation(request));
        }
        
        [Fact]
        public async Task GetListOfVehicleInformation_Should_FilterByModifiedDate()
        {
            var request = new PaginationFilterRequest { PageNumber = 0, PageSize = 0, ModifiedDate = DateTime.Parse("10/22/2022 2:30:15 PM") };
            _mockMyMapper.Setup(x => x.MapVehicles(It.IsAny<List<dataLayer.VehicleInformation>>())).Returns(_mockVehicles);

            _mockRepository.Setup(repo => repo.GetAllVehicles()).Returns(Task.FromResult(_mockRepositoryVehicles));

            //Act
            var result = await _service.GetListOfVehicleInformation(request);

            //Assert
            Assert.NotNull(result);
        }
        
        [Fact]
        public async Task GetListOfVehicleInformation_Should_FilterByDealerId()
        {
            var request = new PaginationFilterRequest { PageNumber = 0, PageSize = 0, DealerId = "12345" };
            _mockMyMapper.Setup(x => x.MapVehicles(It.IsAny<List<dataLayer.VehicleInformation>>())).Returns(_mockVehicles);

            _mockRepository.Setup(repo => repo.GetAllVehicles()).Returns(Task.FromResult(_mockRepositoryVehicles));

            //Act
            var result = await _service.GetListOfVehicleInformation(request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result[0].DealerId, request.DealerId);
        }

        [Fact]
        public async Task PopulateVehicleInformation_Should_Create_Vehicle_Records()
        {
            _mockCsvReader.Setup(x => x.ReadFile(It.IsAny<string>()));
            _mockMyMapper.Setup(x => x.MapVehicles(It.IsAny<List<dataLayer.VehicleInformation>>())).Returns(_mockVehicles);
            _mockRepository.Setup(repo => repo.GetAllVehicles()).Returns(Task.FromResult(_mockRepositoryVehicles));

            var result = await _service.PopulateVehicleInformation("C:\\Temp\temp.csv");

            Assert.True(result);
        }
    }
}
