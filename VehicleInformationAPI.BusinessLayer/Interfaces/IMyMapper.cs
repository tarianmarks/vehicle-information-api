namespace VehicleInformationAPI.BusinessLayer.Interfaces
{
    public interface IMyMapper
    {
        //TDestination Map<TSource, TDestination>(TSource source);
        //List<TDestination> MapList<TSource, TDestination>(List<TSource> source);
        
        Models.VehicleInformation MapVehicle(DataLayer.Models.VehicleInformation vehicle);
        List<Models.VehicleInformation> MapVehicles(List<DataLayer.Models.VehicleInformation> vehicles);
    }
}
