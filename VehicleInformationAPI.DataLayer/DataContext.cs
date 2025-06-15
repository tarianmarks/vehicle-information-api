using Microsoft.EntityFrameworkCore;

//using System.Data.Entity;
using VehicleInformationAPI.DataLayer.Models;

namespace VehicleInformationAPI.DataLayer
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public DataContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<VehicleInformation> VehicleInformations { get; set; }
        public DbSet<VehicleInformationExtended> VehicleInformationsExtended { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var config = modelBuilder.Entity<VehicleInformation>();
            config.ToTable("vehicle_information");
        }

    }

}
