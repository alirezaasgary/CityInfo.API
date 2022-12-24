using CityInfo.API.Models_Entity;

namespace CityInfo.API.Repotories
{
    public interface ICityInfoRepository
    {
        #region City


        //  IEnumerable<City> GetCities();
        Task<IEnumerable<City>> GetCitiesAsync();//در متد های ایسینک سعی شود کلمه ایسینک انتهای متد نوشده شود .
        Task<City?> GetCityAsync(int id, bool pointInterest); //علامت سوال سینتی یعنی ممکن است شهر چیدا نشده باشد و خروجیتسک نال باشد و 

        #endregion


        #region PointOfIntrest

        Task<IEnumerable<PointOfIntrest>> GetPointsAsync(int cityId);
        Task<PointOfIntrest?> GetPointsAsync(int cityId,int pointId);

        #endregion]
    }
}
