using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using API_OCPP.Models;

#nullable disable

namespace API_OCPP.Data
{
    public partial class SqlDbContext : DbContext
    {
        public SqlDbContext()
        {
        }

        public SqlDbContext(DbContextOptions<SqlDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChargingStation> ChargingStations { get; set; }
        public virtual DbSet<Heartbeat> Heartbeats { get; set; }
        public virtual DbSet<User> Users { get; set; }

  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ChargingStation>(entity =>
            {
                entity.Property(e => e.Available).HasMaxLength(20);

                entity.Property(e => e.FirmwareVersion)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("IP");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Port)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.VendorName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Heartbeat>(entity =>
            {
                entity.Property(e => e.Hbtime).HasColumnType("datetime");

                entity.HasOne(d => d.ChargingStations)
                    .WithMany(p => p.Heartbeats)
                    .HasForeignKey(d => d.ChargingStationsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Heartbeat__Charg__66603565");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TagId)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
