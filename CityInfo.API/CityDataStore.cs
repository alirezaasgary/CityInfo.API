using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CityDataStore
    {
        public List<CityDto> Cities { get; set; }

        //public static CityDataStore current { get;}=new CityDataStore();
        public CityDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {Id=1,Name="Moscow",Descriptions="",
                    PointOfIntrests=new List<PointOfIntrestDto>()
                    {
                    new PointOfIntrestDto() { Id=1,Name="meydansorkh" ,Description=""},
                    new PointOfIntrestDto() {Id=2,Name="moscow city" ,Description=""}
                    } 
                },
                new CityDto() {Id=2,Name="Tehran",Descriptions="" 
                ,PointOfIntrests=new List<PointOfIntrestDto>()
                {
                    new PointOfIntrestDto() {Id=3,Name="milad" ,Description=""}
                }
                },
                new CityDto() {Id=3,Name="Room",Descriptions="" }

            };
        }

    }
}

