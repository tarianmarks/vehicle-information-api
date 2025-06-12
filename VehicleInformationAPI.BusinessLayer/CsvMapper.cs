using CsvHelper.Configuration;
using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.BusinessLayer
{
    public class CsvMapper : ClassMap<VehicleInformation>
    {
        public CsvMapper()
        {
            Map(m => m.DealerId).Name("dealerId");
            Map(m => m.Vin).Name("vin");
            Map(m => m.ModifiedDate).Name("modifiedDate");
        }
     
    }
}
