using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//using VehicleInformationAPI.DataAccessLayer;
//using VehicleInformationAPI.DataAccessLayer.Interfaces;
//using VehicleInformationAPI.DataAccessLayer.Repositories;

namespace VehicleInformationAPI.BusinessLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var builder = WebApplication.CreateBuilder(args);
            //builder.Services.AddDbContext<DataContext>(/*options => options.UseSqlServer(connectionString)*/);
            //builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("OpeningStatementDatabase")));
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseStartup<Startup>();
                });
    }
}
