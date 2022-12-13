using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers( option =>
{ 
    
    option.ReturnHttpNotAcceptable=true; //جهت پشتیبانی از فرمت خواص
}).AddXmlDataContractSerializerFormatters(); //جهت پشتیبانی از فرمت xml


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

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
