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
            if ((request.PageNumber != null && request.PageSize == null)
                || (request.PageNumber == null && request.PageSize != null)
                || (request.PageNumber == 0 && request.PageSize != null)
                || (request.PageNumber == 0 && request.PageSize == 0)
                || (request.PageNumber == 0 && request.PageSize == 0)
                || (request.PageNumber == null && request.PageSize == 0)
                || (request.PageNumber != null && request.PageSize == 0)
                || (request.PageNumber == 0 && request.PageSize == null))
            {
                throw new Exception("Both page number and page size must be set in order to use pagination");
            }
            else 
            {
                var resultList = new List<VehicleInformation>();
                
                var result = await _vehicleInformationRepository.GetAllVehicles();

                if (result.Count > 0) {
                    if (request.DealerId != null)
                    {
                        resultList = (List<VehicleInformation>)result.Where(y => y.DealerId == request.DealerId);
                    }

                    if (request.ModifiedDate != null)
                    {
                        resultList = (List<VehicleInformation>)result.Where(y => y.ModifiedDate == request.ModifiedDate);
                    }

                    if (request.PageSize > 0)
                    {
                        var pageSize = resultList.Count % request.PageSize;
                        if (pageSize > 0)
                        {
                            resultList.Take(resultList.Count / request.PageSize + 1).ToList();
                        }
                    }

                    if (request.PageNumber > 0)
                    {
                        resultList.Skip(request.PageNumber).ToList();
                    }
                }
                return resultList;
            }
        }
    }
}
