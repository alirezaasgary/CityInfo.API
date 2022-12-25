using CityInfo.API.Models;
using CityInfo.API.Models_Entity;

namespace CityInfo.API.Repository
{
    public interface ICityInfoRepository
    {
        #region City


        //  IEnumerable<City> GetCities();
        Task<IEnumerable<City>> GetCitiesAsync();//در متد های ایسینک سعی شود کلمه ایسینک انتهای متد نوشده شود .
        Task<City?> GetCityAsync(int id, bool pointInterest); //علامت سوال سینتی یعنی ممکن است شهر چیدا نشده باشد و خروجیتسک نال باشد و 
        Task<bool> CityExistAsync(int cityId);

        #endregion


        #region PointOfIntrest

        Task<IEnumerable<PointOfIntrest>> GetPointsAsync(int cityId);
        Task<PointOfIntrest?> GetPointAsync(int cityId,int pointId);

        Task<bool> PointOfIntrestExistAsync(int cityId, int pointId);

        Task AddPointOfIntrestAsync(int cityId, PointOfIntrest pointOfIntrest);

        Task UpdatePointOfIntrestAsync(int cityId, PointOfIntrest pointOfIntrest);

        void DeletePointOfIntrestAsync(PointOfIntrest pointOfIntrest);

        Task<bool> SaveChangAsync();
        #endregion]
    }
}
