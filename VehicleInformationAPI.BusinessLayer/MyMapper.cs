//using AutoMapper;
using VehicleInformationAPI.BusinessLayer.BusinessObjects;
using mainModels = VehicleInformationAPI.Models;
using dataModels = VehicleInformationAPI.DataLayer.Models;
using VehicleInformationAPI.DataLayer.Models;
using CsvHelper.Configuration;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using System.Collections.Generic;
using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.BusinessLayer
{
    public class MyMapper : IMyMapper
    {
        public MyMapper() { }

        public Models.VehicleInformation MapVehicle(DataLayer.Models.VehicleInformation vehicle)
        {
            return new Models.VehicleInformation()
            {
                DealerId = vehicle.dealer_Id,
                Vin = vehicle.vin,
                ModifiedDate = vehicle.modified_date
            };
        }

        public List<Models.VehicleInformation> MapVehicles(List<DataLayer.Models.VehicleInformation> vehicles)
        {
            var mappedVehicles = new List<Models.VehicleInformation>();

            foreach (var vehicle in vehicles)
            {
                mappedVehicles.Add(new Models.VehicleInformation()
                {
                    DealerId = vehicle.dealer_Id,
                    Vin = vehicle.vin,
                    ModifiedDate = vehicle.modified_date
                });
            }
            return mappedVehicles;
        }
        public List<DataLayer.Models.VehicleInformation> MapVehiclesToDb(List<Models.VehicleInformation> vehicles)
        {
            var mappedVehicles = new List<DataLayer.Models.VehicleInformation>();

            foreach (var vehicle in vehicles)
            {
                mappedVehicles.Add(new DataLayer.Models.VehicleInformation()
                {
                    dealer_Id = vehicle.DealerId,
                    vin = vehicle.Vin!,
                    modified_date = vehicle.ModifiedDate
                });
            }
            return mappedVehicles;
        }
    }
}
