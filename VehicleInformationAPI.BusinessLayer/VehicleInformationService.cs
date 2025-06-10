using AutoMapper;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.DataLayer.Interfaces;
using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.BusinessLayer
{
    public class VehicleInformationService(IVehicleInformationRepository vehicleInformationRepository, IMapper mapper, ILogger<IVehicleInformationService> logger) : IVehicleInformationService
    {
        private readonly IVehicleInformationRepository _vehicleInformationRepository = vehicleInformationRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<IVehicleInformationService> _logger = logger;

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
            //return _mapper.Map<Models.Client>(await _clientRepository.GetClient(id));
        }

        /// <summary>
        /// Stores vehicle information into data store
        /// </summary>
        /// <returns>The vehicle information found with the VIN</returns>
        public async Task<VehicleInformation> GetVehicleInformationByVIN(string vin)
        {
            return await _vehicleInformationRepository.GetVehicleInformationByVIN(vin);
            //return _mapper.Map<VehicleInformation>(await _vehicleInformationRepository.GetVehicleInformationByVIN(vin));
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

            if (result.Count > 0) {                    
            //Pagination
                if (request.PageSize > 0 && request.PageNumber > 0)
                {
                    if(request.PageNumber == 1)
                    {
                        resultList = result.Take(request.PageSize).ToList();
                    }

                    else
                    {
                        resultList = result.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
                    }
                } 
                
                else if (request.PageSize == 0 || request.PageNumber == 0)
                {
                    // no page size or page number specified, so just return all of the results
                    resultList = result;
                }

                else if (request.PageSize < 0 || request.PageNumber < 0)
                {
                    throw new Exception("Page Size and page numbers must not be negative");
                }

            // Filtering
                if (request.DealerId > 0)
                {
                    var dealers = resultList.Where(x=> x.DealerId == request.DealerId).ToList();
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
    }
}
