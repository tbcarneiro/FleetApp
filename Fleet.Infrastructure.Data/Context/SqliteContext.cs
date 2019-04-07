using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleet.Domain.Entities;
using Fleet.Infrastructure.Data.Mapping;
using Microsoft.Data.Sqlite;

namespace Fleet.Infrastructure.Data.Context
{
    public class SqliteContext : DbContext
    {
        public DbSet<Chassis> Chassis { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());

            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite(connection);           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chassis>(new ChassisMap().Configure);
            modelBuilder.Entity<VehicleType>(new VehicleTypeMap().Configure);
            modelBuilder.Entity<Vehicle>(new VehicleMap().Configure);
        }
    }
}
