using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace BO.Models
{
    public partial class HospitalManagementDBContext : DbContext
    {
        public HospitalManagementDBContext()
        {
        }

        public HospitalManagementDBContext(DbContextOptions<HospitalManagementDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<DoctorInformation> DoctorInformations { get; set; } = null!;
        public virtual DbSet<StaffAccount> StaffAccounts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
                //tracking update
                //optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }
        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
            var strConn = config.GetConnectionString("DBConnect");
            return strConn;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId).ValueGeneratedNever();

                entity.Property(e => e.DepartmentLocation).HasMaxLength(250);

                entity.Property(e => e.DepartmentName).HasMaxLength(120);

                entity.Property(e => e.ShortDescription).HasMaxLength(250);

                entity.Property(e => e.TelephoneNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<DoctorInformation>(entity =>
            {
                entity.HasKey(e => e.DoctorId)
                    .HasName("PK__DoctorIn__2DC00EDFCA1E7A16");

                entity.ToTable("DoctorInformation");

                entity.Property(e => e.DoctorId)
                    .HasMaxLength(20)
                    .HasColumnName("DoctorID");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DoctorAddress).HasMaxLength(200);

                entity.Property(e => e.DoctorLicenseNumber).HasMaxLength(25);

                entity.Property(e => e.DoctorName).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DoctorInformations)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__DoctorInf__Depar__3B75D760");
            });

            modelBuilder.Entity<StaffAccount>(entity =>
            {
                entity.HasKey(e => e.HraccountId)
                    .HasName("PK__StaffAcc__9A64147CFE3BAFC6");

                entity.ToTable("StaffAccount");

                entity.Property(e => e.HraccountId)
                    .HasMaxLength(50)
                    .HasColumnName("HRAccountId");

                entity.Property(e => e.Hremail)
                    .HasMaxLength(50)
                    .HasColumnName("HREmail");

                entity.Property(e => e.Hrfullname)
                    .HasMaxLength(50)
                    .HasColumnName("HRFullname");

                entity.Property(e => e.Hrpassword)
                    .HasMaxLength(50)
                    .HasColumnName("HRPassword");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
