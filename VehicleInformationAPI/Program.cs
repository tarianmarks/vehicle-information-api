//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
using AutoMapper;
using VehicleInformationAPI.BusinessLayer;
using VehicleInformationAPI.BusinessLayer.Interfaces;
//using VehicleInformationAPI.DataLayer;
using VehicleInformationAPI.DataLayer.Interfaces;
using VehicleInformationAPI.DataLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(VehicleInformationService));
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IVehicleInformationService, VehicleInformationService>();
builder.Services.AddScoped<IVehicleInformationRepository, VehicleInformationRepository>();
//builder.Services.AddScoped<IClient, Client>();
//builder.Services.AddScoped<DbContext, DataContext>();
//builder.Services.AddDbContext<DataContext>(/*options =>
//    options.UseSqlServer(connectionString)*/);
//builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("OpeningStatementDatabase")));


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
