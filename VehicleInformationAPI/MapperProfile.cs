using AutoMapper;
using VehicleInformationAPI.BusinessLayer.BusinessObjects;
using mainModels = VehicleInformationAPI.Models;
using dataModels = VehicleInformationAPI.DataLayer.Models;
using VehicleInformationAPI.DataLayer.Models;

namespace VehicleInformationAPI
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //var configuration = new MapperConfiguration(cfg =>
            //{
            //    //cfg.CreateMap<VehicleInformationBO, mainModels.VehicleInformation>();
            //    //cfg.CreateMap<VehicleInformationBO, dataModels.VehicleInformation>();
            //    cfg.CreateMap<dataModels.VehicleInformation, mainModels.VehicleInformation>().ReverseMap();

            //    //cfg.AddMaps(new[]
            //    //{
            //    //    typeof(VehicleInformationService),
            //    //});

            //    //cfg.AddProfile<Mapper>();
            //    //cfg.AddMaps(typeof(VehicleInformationService));
            //});

            CreateMap<dataModels.VehicleInformation, mainModels.VehicleInformation>().ReverseMap();
        }
    }
}
