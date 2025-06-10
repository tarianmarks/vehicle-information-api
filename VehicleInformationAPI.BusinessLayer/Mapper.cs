using AutoMapper;
using VehicleInformationAPI.BusinessLayer.BusinessObjects;
using mainModels = VehicleInformationAPI.Models;
//using dataModels = VehicleInformationAPI.DataLayer.Models;

namespace VehicleInformationAPI.BusinessLayer
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VehicleInformationBO, mainModels.VehicleInformation>();
                //cfg.CreateMap<mainModels.Dashboard, dataModels.Dashboard>();
                //cfg.CreateMap<dataModels.Dashboard, mainModels.Dashboard>();
            });
        }
    }
}
