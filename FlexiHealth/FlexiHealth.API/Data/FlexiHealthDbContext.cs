using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FlexiHealth.API.Data;

public partial class FlexiHealthDbContext : DbContext
{
    public FlexiHealthDbContext()
    {
    }

    public FlexiHealthDbContext(DbContextOptions<FlexiHealthDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.AppointmentDateTime).HasColumnType("datetime");
            entity.Property(e => e.AppointmentId).ValueGeneratedOnAdd();
            entity.Property(e => e.AppointmentType).HasMaxLength(50);
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Notes).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Surname)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.Doctor).WithMany()
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_Appointments_ToTable");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctors__2DC00EBFAEAB06BA");

            entity.Property(e => e.Bio).HasMaxLength(100);
            entity.Property(e => e.DateJoined).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.ProfileImageUrl).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
