using CityInfo.API.Models_Entity;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Context
{
    public class CityInfoContext:DbContext
    {
        public CityInfoContext(DbContextOptions<CityInfoContext> options):base(options)
        {

        }
        public DbSet<City> City { get; set; } = null!;// به دلیل کانستراکتور کلاس سیتی هشدار میدهد و ما با این کد میفهمانیم که منترل شده است .
        public DbSet<PointOfIntrest> PointOfIntrest { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite();//مشخص میکنیم که از چه بانک اطلاعاتی استفاده میکنیم 
        //    //یک راه دیگر این است که در کلاس پرگرام این تنظیمات را استفاده کنیم 
        //    base.OnConfiguring(optionsBuilder);
        //}

    }
}
