using CityInfo.API.Models_Entity;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Context
{
    public class CityInfoContext:DbContext
    {
        public CityInfoContext()
        {

        }
        public DbSet<City> City { get; set; } = null!;// به دلیل کانستراکتور کلاس سیتی هشدار میدهد و ما با این کد میفهمانیم که منترل شده است .
        public DbSet<PointOfIntrest> PointOfIntrest { get; set; } = null!;

    }
}
