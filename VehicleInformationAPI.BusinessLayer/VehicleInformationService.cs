using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
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

        public async Task<List<VehicleInformationExtended>> GetExtendedVehicleInformation()
        {
            //get data from the database
            var results = _vehicleInformationRepository.GetAllVehicles().Result;

            var vins = string.Join(";", results.Select(x => x.vin!).Distinct());

            string url = @"https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVINValuesBatch/";
            var nameValues = new Dictionary<string, string>
            {
                { "data", vins },
                { "format", "json" }
            };

            _httpClient.BaseAddress = new Uri(url);

            // using FormUrlEncodedContent
            var name = new FormUrlEncodedContent(nameValues);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var token = new CancellationToken();

            try
            {
                var tmp = _httpClient.PostAsync(_httpClient.BaseAddress, name, token).Result;

                if (tmp.IsSuccessStatusCode)
                {
                    var result = await tmp.Content.ReadFromJsonAsync<NhtsaResults>();
                    var vehicleExt = result!.Results;

                    foreach(var res in results)
                    {
                        if (vehicleExt!.Count > 0)
                        {
                            foreach(var vex in vehicleExt)
                            {
                                if(vex.Vin == res.vin)
                                {
                                    vex.ModifiedDate = res.modified_date;
                                    vex.DealerId = res.dealer_Id;
                                }
                            }
                        }
                        
                    }

                    return vehicleExt!;
                }

                else
                {
                    _logger.LogError("Can't retrieve information from NHTSA");
                    return new List<VehicleInformationExtended>();
                }
            }
            catch (Exception err)
            {
                _logger.LogError($"Error in NHTSA API attempt {err}");
                throw new Exception($"Error in NHTSA API attempt {err}");
            }
        }

        public async Task<List<VehicleInformationExtended>> PopulateVehicleInformation(string csvFile)
        {
            try
            {
                //pull vehicle information from the csv file and store it in the database
                var vehicleInformation = _csvReader.ReadFile(csvFile);

                //query the database for the records
                var vehicles = await _vehicleInformationRepository.GetAllVehicles();
                //pull vehicle information from the nhtsa api
                var vehicleInformationExtended = await GetExtendedVehicleInformation();

                //return vehicle information from db, augmented with nhtsa info
    
                foreach (var vehicle in vehicles)
                {
                    if (vehicleInformationExtended!.Count > 0)
                    {
                        foreach (var vex in vehicleInformationExtended)
                        {
                            if (vex.Vin == vehicle.vin)
                            {
                                vex.ModifiedDate = vehicle.modified_date;
                                vex.DealerId = vehicle.dealer_Id!;
                            }
                        }
                    }
                }
                return vehicleInformationExtended;
            }

            catch (Exception err)
            {
                throw new Exception($"Unable to augment vehicle information. {err}");
            }
        }
    }
}
