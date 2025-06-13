namespace VehicleInformationAPI.BusinessLayer.Interfaces
{
    public interface ISaveToFileStorage
    {
        public Task UploadFile(byte[] fileContent, string filename);
    }
}
