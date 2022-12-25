﻿using CityInfo.API;
using CityInfo.API.Context;
using CityInfo.API.Repository;
using CityInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Serilog;

//تنظیمات لاگ زدن 
Log.Logger=new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/CityInfo.txt",rollingInterval:RollingInterval.Day)
    .CreateLogger(); 

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(); //فعال کردن سری لاگ.

// Add services to the container.

builder.Services.AddControllers( option =>
{ 
    
    option.ReturnHttpNotAcceptable=true; //جهت پشتیبانی از فرمت خواص
}).AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters(); //جهت پشتیبانی از فرمت xml


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
builder.Services.AddScoped<ILocalMailService,LocalMailService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // اگر پروژه دیگری نداشته باشیم .

builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();

builder.Services.AddSingleton<CityDataStore>();

builder.Services.AddDbContext<CityInfoContext>(option =>
{
    option.UseSqlite(builder.Configuration["ConnectionString:SQLightConnectionString"]);
});
//Addsingleton : یک عدد نمونه سازی کن و بده همه استفاده کنن
//AddScope : به ازای هر کار بر یک نمونه بساز و بده استفاده کنه


var app = builder.Build();

// Configure the HTTP request pipeline.


#region Pipline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    
});


#endregion

app.Run();
