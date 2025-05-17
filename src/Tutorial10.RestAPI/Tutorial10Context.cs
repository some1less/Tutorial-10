using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tutorial10.RestAPI;

public partial class Tutorial10Context : DbContext
{
    public Tutorial10Context()
    {
    }

    public Tutorial10Context(DbContextOptions<Tutorial10Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Departemnt> Departemnts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departemnt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departem__3214EC07D7F41538");

            entity.ToTable("Departemnt");

            entity.HasIndex(e => e.Name, "UQ__Departem__737584F60F5D40F1").IsUnique();

            entity.Property(e => e.Location)
                .HasMaxLength(33)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07CA1C1291");

            entity.ToTable("Employee");

            entity.Property(e => e.Commission).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("decimal(7, 2)");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__Depart__3F466844");

            entity.HasOne(d => d.Job).WithMany(p => p.Employees)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__JobId__3D5E1FD2");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__Employee__Manage__3E52440B");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Job__3214EC07EF7F79CD");

            entity.ToTable("Job");

            entity.HasIndex(e => e.Name, "UQ__Job__737584F6EC94DFB3").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
