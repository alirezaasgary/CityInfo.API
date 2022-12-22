namespace CityInfo.API.Services
{
    public interface ILocalMailService
    {
        void Send(string subject, string message);
    }
}