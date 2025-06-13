using CsvHelper;
using System.Globalization;
using System;
using VehicleInformationAPI.BusinessLayer.BusinessObjects;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.Models;
using VehicleInformationAPI.DataLayer.Interfaces;

namespace VehicleInformationAPI.BusinessLayer
{
    public class ReadFromCsv(IVehicleInformationRepository vehicleInformationRepository, IMyMapper myMapper, ILogger<IVehicleInformationService> logger, ISaveToFileStorage saveToFileStorage) : IReadFromCsv
    {
        private readonly IVehicleInformationRepository _vehicleInformationRepository = vehicleInformationRepository;
        private readonly IMyMapper _myMapper = myMapper;
        private readonly ILogger<IVehicleInformationService> _logger = logger;
        private readonly ISaveToFileStorage _saveToFileStorage = saveToFileStorage;

        public async Task<List<VehicleInformation>> ReadFile(string csvFile)
        {
            string directoryPath;

            if (string.IsNullOrEmpty(csvFile))
            {
                directoryPath = @"C:\Temp\";
                csvFile = @"C:\Temp\sample-vin-data.csv";
            }

            string[] filePath = csvFile.Split("\\");
            string name = filePath[filePath.Length - 1];
            directoryPath = string.Join("\\", filePath.Take(filePath.Length - 1));

            if (name.Contains(".csv"))
            {
                //using (var reader = new StreamReader(Path.Combine(directoryPath, csvFile)))
                using (var reader = new StreamReader(csvFile))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    try
                    {
                        //map headers
                        csv.Context.RegisterClassMap<CsvMapper>();

                        var records = csv.GetRecords<VehicleInformation>().ToList();

                        //map values 
                        //store each vehicle in the queryable data store.
                        await _vehicleInformationRepository.StoreVehicleInformation(_myMapper.MapVehiclesToDb(records));

                        //the CSV file should be archived in a backing file store
                        var fileContent = File.ReadAllBytes(csvFile);

                        //await _saveToFileStorage.UploadFile(csvFile, directoryPath);
                        await _saveToFileStorage.UploadFile(fileContent, name);

                        return records;
                    }

                    catch (Exception ex)
                    {
                        _logger.LogError($"Problem processing csv file {ex}");
                        throw new Exception($"Problem processing csv file {ex}");
                    }
                }
            }

            else
            {
                _logger.LogError($"Unable to read the file {name}: It's not a .csv file");
                throw new Exception($"Unable to read the file {name}: It's not a .csv file");
            }

        }
    }
}
