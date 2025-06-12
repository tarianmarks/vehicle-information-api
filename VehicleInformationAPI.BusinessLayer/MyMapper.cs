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
    //public class Mapper : Profile
    //{
    //    public Mapper()
    //    {
    //        //var configuration = new MapperConfiguration(cfg =>
    //        //{
    //        //    //cfg.CreateMap<VehicleInformationBO, mainModels.VehicleInformation>();
    //        //    //cfg.CreateMap<VehicleInformationBO, dataModels.VehicleInformation>();
    //        //    cfg.CreateMap<dataModels.VehicleInformation, mainModels.VehicleInformation>().ReverseMap();

    //        //    //cfg.AddMaps(new[]
    //        //    //{
    //        //    //    typeof(VehicleInformationService),
    //        //    //});

    //        //    //cfg.AddProfile<Mapper>();
    //        //    //cfg.AddMaps(typeof(VehicleInformationService));
    //        //});

    //        CreateMap<dataModels.VehicleInformation, mainModels.VehicleInformation>().ReverseMap();
    //    }
    //}

    public class MyMapper : IMyMapper
    {
        public MyMapper() { }
        
        public Models.VehicleInformation MapVehicle(DataLayer.Models.VehicleInformation vehicle)
        {
            return new Models.VehicleInformation()
            {
                DealerId = vehicle.DealerId,
                Vin = vehicle.Vin,
                ModifiedDate = vehicle.ModifiedDate
            };
        }

        public List<Models.VehicleInformation> MapVehicles(List<DataLayer.Models.VehicleInformation> vehicles)
        {
            var mappedVehicles = new List<Models.VehicleInformation>();

            foreach (var vehicle in vehicles)
            {
                mappedVehicles.Add(new Models.VehicleInformation()
                {
                    DealerId = vehicle.DealerId,
                    Vin = vehicle.Vin,
                    ModifiedDate = vehicle.ModifiedDate
                });
            }
            return mappedVehicles;
        }
    }
}
