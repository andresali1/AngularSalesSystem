using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SalesSystem.Model;

namespace SalesSystem.DAL.DBContext;

public partial class DbsalesContext : DbContext
{
    public DbsalesContext()
    {
    }

    public DbsalesContext(DbContextOptions<DbsalesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppUser> AppUsers { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<DocumentNumber> DocumentNumbers { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<RoleMenu> RoleMenus { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleDetail> SaleDetails { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__App_User__1788CC4C0A7662EF");

            entity.ToTable("App_User");

            entity.Property(e => e.CompleteName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.Pass)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Role).WithMany(p => p.AppUsers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__App_User__RoleId__3F466844");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B83F3A03D");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<DocumentNumber>(entity =>
        {
            entity.HasKey(e => e.DocumentNumberId).HasName("PK__Document__0D8685FFF60F455A");

            entity.ToTable("DocumentNumber");

            entity.Property(e => e.LastNumber).HasColumnName("Last_Number");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PK__Menu__C99ED23099449A8A");

            entity.ToTable("Menu");

            entity.Property(e => e.Icon)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MenuName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MenuUrl)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CD9FA3BCAA");

            entity.ToTable("Product");

            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Product__Categor__47DBAE45");
        });

        modelBuilder.Entity<RoleMenu>(entity =>
        {
            entity.HasKey(e => e.RoleMenuId).HasName("PK__RoleMenu__F86287B61AC7F2FF");

            entity.ToTable("RoleMenu");

            entity.HasOne(d => d.Menu).WithMany(p => p.RoleMenus)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("FK__RoleMenu__MenuId__3B75D760");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleMenus)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__RoleMenu__RoleId__3C69FB99");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Sale__1EE3C3FFFA1C94CF");

            entity.ToTable("Sale");

            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.PaymentType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<SaleDetail>(entity =>
        {
            entity.HasKey(e => e.SaleDetailId).HasName("PK__SaleDeta__70DB14FEE34BE318");

            entity.ToTable("SaleDetail");

            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__SaleDetai__Produ__534D60F1");

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.SaleId)
                .HasConstraintName("FK__SaleDetai__SaleI__52593CB8");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__User_Rol__8AFACE1AEA68CCE5");

            entity.ToTable("User_Role");

            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
