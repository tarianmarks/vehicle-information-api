using CsvHelper;
using System.Globalization;
using System;
using VehicleInformationAPI.BusinessLayer.BusinessObjects;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.Models;
using AutoMapper;
using VehicleInformationAPI.DataLayer.Interfaces;

namespace VehicleInformationAPI.BusinessLayer
{
    public class ReadFromCsv(IVehicleInformationRepository vehicleInformationRepository, IMapper mapper, ILogger<IVehicleInformationService> logger) : IReadFromCsv
    {
        private readonly IVehicleInformationRepository _vehicleInformationRepository = vehicleInformationRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<IVehicleInformationService> _logger = logger;

        //public VehicleInformationBO ReadFile(string csvFile)
        public List<VehicleInformation> ReadFile(string csvFile)
        {
            var directoryPath = @"C:\Users\taria\Documents\clients\kwelity\apps\evn\";

            using (var reader = new StreamReader(Path.Combine(directoryPath, csvFile)))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                //map headers
                csv.Context.RegisterClassMap<CsvMapper>();

                var records = csv.GetRecords<VehicleInformation>().ToList();
                //map values

                //store each vehicle in the queryable data store.
                _vehicleInformationRepository.StoreVehicleInformation(records);

                //the CSV file should be archived in a backing file store

                return records;
            }

        }
    }
}
