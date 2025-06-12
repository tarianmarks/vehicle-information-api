using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using AutoMapper;
using VehicleInformationAPI.BusinessLayer;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.DataLayer;

//using VehicleInformationAPI.DataLayer;
using VehicleInformationAPI.DataLayer.Interfaces;
using VehicleInformationAPI.DataLayer.Repositories;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using VehicleInformationAPI;
using Microsoft.EntityFrameworkCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MapperProfile));
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(new[] { typeof(VehicleInformationAPI.BusinessLayer).Assembly });

//builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<IVehicleInformationService, VehicleInformationService>();
builder.Services.AddScoped<IVehicleInformationRepository, VehicleInformationRepository>();
//builder.Services.AddScoped<DbContext, DataContext>();
builder.Services.AddScoped<IReadFromCsv, ReadFromCsv>();
builder.Services.AddScoped<IMyMapper, MyMapper>();
builder.Services.AddHttpClient();
//builder.Services.AddScoped<IClient, Client>();
//builder.Services.AddScoped<DbContext, DataContext>();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("VehicleInformationDb")));

//Automapper config
//var configuration = new MapperConfiguration(cfg =>
//{
//    //cfg.AddProfile(Mapper);
//    //cfg.AddMaps(new[]
//    //{
//    //    typeof(VehicleInformationService),
//    //});

//    //cfg.AddProfile<MapperProfile>();
//    //cfg.AddMaps(typeof(VehicleInformationService));
//});
//IMapper mapper = configuration.CreateMapper();
//builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
