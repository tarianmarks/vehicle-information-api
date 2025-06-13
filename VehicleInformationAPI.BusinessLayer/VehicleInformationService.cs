using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.DataLayer.Interfaces;
using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.BusinessLayer
{
    public class VehicleInformationService(IVehicleInformationRepository vehicleInformationRepository, IMyMapper myMapper,
        IReadFromCsv csvReader, ILogger<IVehicleInformationService> logger, HttpClient httpClient) : IVehicleInformationService
    {
        private readonly IVehicleInformationRepository _vehicleInformationRepository = vehicleInformationRepository;
        private readonly IReadFromCsv _csvReader = csvReader;
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILogger<IVehicleInformationService> _logger = logger;
        private readonly IMyMapper _myMapper = myMapper;

        //public VehicleInformation? GetVehicleInformation(string vin)
        //{
        //    return null;
        //}

        /// <summary>
        /// Stores vehicle information into data store
        /// </summary>
        /// <returns></returns>
        public async Task<VehicleInformation> StoreVehicleInDataStore(VehicleInformation vehicle)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stores vehicle information into data store
        /// </summary>
        /// <returns>The vehicle information found with the VIN</returns>
        public async Task<VehicleInformation> GetVehicleInformationByVin(string vin)
        {
            return _myMapper.MapVehicle(await _vehicleInformationRepository.GetVehicleInformationByVin(vin));
        }

        ///<summary>
        ///Allow for setting the page size and page number of the results.
		///Allow for filtering on a specific dealerId, or after a modifiedDate.
        ///</summary>
        ///
        public async Task<List<VehicleInformation>> GetListOfVehicleInformation(PaginationFilterRequest request)
        {
            var resultList = new List<VehicleInformation>();
            var result = await _vehicleInformationRepository.GetAllVehicles();

            var mapped = _myMapper.MapVehicles(result);

            if (mapped.Count > 0)
            {
                //Pagination
                if (request.PageSize > 0 && request.PageNumber > 0)
                {
                    if (request.PageNumber == 1)
                    {
                        resultList = mapped.Take(request.PageSize).ToList();
                    }

                    else
                    {
                        resultList = mapped.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
                    }
                }

                else if (request.PageSize == 0 || request.PageNumber == 0)
                {
                    // no page size or page number specified, so just return all of the results
                    resultList = mapped;
                }

                else if (request.PageSize < 0 || request.PageNumber < 0)
                {
                    throw new Exception("Page Size and page numbers must not be negative");
                }

                // Filtering
                if (!string.IsNullOrEmpty(request.DealerId))
                {
                    var dealers = resultList.Where(x => x.DealerId == request.DealerId).ToList();
                    resultList = dealers;
                }

                if (!string.IsNullOrEmpty(request.ModifiedDate.ToString()))
                {
                    //var values = result.Where(x => x.ModifiedDate >= request.ModifiedDate).ToList();
                    var values = resultList.Where(x => x.ModifiedDate >= request.ModifiedDate).ToList();
                    resultList = values;
                }
            }
            return resultList;
        }

        public async Task<List<NhtsaResults>> GetExtendedVehicleInformation()
        {
            var results = _vehicleInformationRepository.GetAllVehicles().Result;

            var vins = string.Join(";", results.Select(x => x.Vin!).Distinct());

            string url = @"https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVINValuesBatch/";
            var nameValues = new Dictionary<string, string>();
            nameValues.Add("data", vins);
            nameValues.Add("format", "json");

            _httpClient.BaseAddress = new Uri(url);

            // using FormUrlEncodedContent
            var name = new FormUrlEncodedContent(nameValues);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            CancellationToken token = new CancellationToken();

            try
            {
                var tmp = _httpClient.PostAsync(_httpClient.BaseAddress, name, token).Result;

                if (tmp.IsSuccessStatusCode)
                {
                    var result = await tmp.Content.ReadFromJsonAsync<List<NhtsaResults>>();

                    return result!;
                }

                else
                {
                    _logger.LogError("Can't retrieve information from NHTSA");
                    return new List<NhtsaResults>();
                }
            }
            catch (Exception err)
            {
                _logger.LogError($"Error in NHTSA API attempt {err}");
                throw new Exception($"Error in NHTSA API attempt {err}");
            }
        }

        public async Task<bool> PopulateVehicleInformation(string csvFile)
        {
            try
            {
                //pull vehicle information from the csv file and store it in the database
                var vehicleInformation = _csvReader.ReadFile(csvFile);

                //query the database for the records

                //pull vehicle information from the nhtsa api
                var vehicleInformationExtended = await this.GetExtendedVehicleInformation();

                //store combined information in the database

                return true;
            }

            catch (Exception err)
            {
                throw new Exception($"Unable to get and store vehicle information into the data store {err}");
            }
        }
    }
}
