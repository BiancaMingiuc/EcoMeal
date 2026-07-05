using System;
using System.Collections.Generic;
using EcoMeal1.Enitites;
using EcoMeal1.Entities;
//using EcoMeal1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace EcoMeal1.Database;

public partial class EcoMealDbContext : IdentityDbContext<UserEcoMeal>
{
    public EcoMealDbContext(DbContextOptions<EcoMealDbContext> options)
    : base(options)
    {}
    
    public virtual DbSet<Business> Businesses { get; set; }

    public virtual DbSet<BusinessType> BusinessTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderPackage> OrderPackages { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<PackageType> PackageTypes { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<UserEcoMeal> UserEcoMeals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Business>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Business__3214EC27FB830A11");

            entity.ToTable("Business");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.BusinessName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ImageURL");

            entity.HasOne(d => d.BusinessType).WithMany(p => p.Businesses)
                .HasForeignKey(d => d.BusinessTypeId)
                .HasConstraintName("FK__Business__Busine__0A688BB1");
        });

        modelBuilder.Entity<BusinessType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Business__3214EC27DD0296A6");

            entity.ToTable("BusinessType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC27FF8A297B");

            entity.ToTable("Order");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BusinessId).HasColumnName("BusinessID");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Business).WithMany(p => p.Orders)
                .HasForeignKey(d => d.BusinessId)
                .HasConstraintName("FK__Order__BusinessI__15DA3E5D");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Order__StatusID__16CE6296");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Order__UserID__14E61A24");
        });

        modelBuilder.Entity<OrderPackage>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.PackageId }).HasName("PK__OrderPac__10B258F1F3D7E139");

            entity.ToTable("OrderPackage");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderPackages)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderPack__Order__19AACF41");

            entity.HasOne(d => d.Package).WithMany(p => p.OrderPackages)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderPack__Packa__1A9EF37A");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Package__3214EC27FEA61BE0");

            entity.ToTable("Package");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BusinessId).HasColumnName("BusinessID");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.PackageName).HasMaxLength(50);
            entity.Property(e => e.PackageTypeId).HasColumnName("PackageTypeID");
            entity.Property(e => e.PickupEnd).HasColumnType("datetime");
            entity.Property(e => e.PickupStart).HasColumnType("datetime");

            entity.HasOne(d => d.Business).WithMany(p => p.Packages)
                .HasForeignKey(d => d.BusinessId)
                .HasConstraintName("FK__Package__Busines__0F2D40CE");

            entity.HasOne(d => d.PackageType).WithMany(p => p.Packages)
                .HasForeignKey(d => d.PackageTypeId)
                .HasConstraintName("FK__Package__Package__10216507");
        });

        modelBuilder.Entity<PackageType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PackageT__3214EC27A7CAFCC9");

            entity.ToTable("PackageType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC27E3975146");

            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Status__3214EC27E76E79B2");

            entity.ToTable("Status");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserEcoMeal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserEcoM__3214EC2726E71146");

            entity.ToTable("UserEcoMeal");

            entity.HasIndex(e => e.Email, "UQ__UserEcoM__A9D10534622CF0C6").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.UserEcoMeals)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserEcoMe__RoleI__05A3D694");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
