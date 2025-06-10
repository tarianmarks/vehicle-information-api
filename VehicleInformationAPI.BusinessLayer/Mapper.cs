using AutoMapper;
using VehicleInformationAPI.BusinessLayer.BusinessObjects;
using VehicleInformationAPI.Models;
using VehicleInformationAPI.DataLayer.Models;

namespace VehicleInformationAPI.BusinessLayer
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<VehicleInformationBO, mainModels.VehicleInformation>();
                //cfg.CreateMap<VehicleInformationBO, dataModels.VehicleInformation>();
                cfg.CreateMap<VehicleInformation, VehicleInformationDataObject>().ReverseMap();
                //cfg.CreateMap<VehicleInformationDataObject, VehicleInformation>();
            });
        }
    }
}
