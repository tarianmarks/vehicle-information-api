using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

//using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using VehicleInformationAPI.DataLayer.Models;

namespace VehicleInformationAPI.DataLayer
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public DataContext(IConfiguration configuration, DbContextOptions<DataContext> options) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<VehicleInformation> VehicleInformations { get; set; }

        //public bool IsSqlParameterNull(SqlParameter param)
        //{
        //    var sqlValue = param.SqlValue;
        //    var nullableValue = sqlValue as INullable;
        //    if (nullableValue != null)
        //        return nullableValue.IsNull;
        //    return sqlValue == null || sqlValue == DBNull.Value;
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var config = modelBuilder.Entity<VehicleInformation>();
            config.ToTable("vehicle_information");

            //modelBuilder.ApplyConfiguration(new sys_DatabaseFirewallRuleConfiguration());
            //modelBuilder.ApplyConfiguration(new VehicleInformationConfiguration());
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }

}
