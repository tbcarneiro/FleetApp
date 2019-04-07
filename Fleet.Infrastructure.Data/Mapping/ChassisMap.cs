using Fleet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.Infrastructure.Data.Mapping
{
    public class ChassisMap : IEntityTypeConfiguration<Chassis>
    {
        public void Configure(EntityTypeBuilder<Chassis> builder)
        {
            builder.ToTable("Chassis");

            builder.HasKey(c => c.VehicleID)
                .HasName("VehicleID");

            builder.Property(c => c.Series)
                .IsRequired()
                .HasColumnName("Series");

            builder.Property(c => c.Number)
                .IsRequired()
                .HasColumnName("Number");
        }
    }
}
