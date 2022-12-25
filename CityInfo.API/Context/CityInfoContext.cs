using CityInfo.API.Models_Entity;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Context
{
    public class CityInfoContext : DbContext
    {
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("MS")
                {
                    Description = "tehran des",
                    Id = 1

                },
                new City("LA")
                {
                    Description = "tehran des",
                    Id = 2

                },
                new City("NY")
                {
                    Description = "tehran des",
                    Id = 3

                });
            modelBuilder.Entity<PointOfIntrest>().HasData(
                new PointOfIntrest("Cr")
                {
                    Description = "",
                    Id = 1,
                    CityId = 1
                },
                new PointOfIntrest("river")
                {
                    Description = "",
                    Id = 2,
                    CityId = 1
                },
                new PointOfIntrest("tower")
                {
                    Description = "",
                    Id = 3,
                    CityId = 2
                }, new PointOfIntrest("park")
                {
                    Description = "",
                    Id = 4,
                    CityId = 2
                }, new PointOfIntrest("muse")
                {
                    Description = "",
                    Id = 5,
                    CityId = 3
                }, new PointOfIntrest("manufactur")
                {
                    Description = "",
                    Id = 6,
                    CityId = 3
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
