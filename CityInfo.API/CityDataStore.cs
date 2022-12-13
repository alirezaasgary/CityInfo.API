using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CityDataStore
    {
        public List<CityDto> Cities { get; set; }

        public static CityDataStore current { get;}=new CityDataStore();
        public CityDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto() {Id=1,Name="Moscow",Description="" },
                new CityDto() {Id=2,Name="Tehran",Description="" },
                new CityDto() {Id=3,Name="Room",Description="" }

            };
        }

    }
}

