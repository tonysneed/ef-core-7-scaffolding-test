using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ScaffoldingTest.Models;

namespace ScaffoldingTest.Contexts;

public partial class NorthwindSlimContext : DbContext
{
    public NorthwindSlimContext()
    {
    }

    public NorthwindSlimContext(DbContextOptions<NorthwindSlimContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerSetting> CustomerSettings { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Territory> Territories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=NorthwindSlim; Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_Categories");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryName).HasMaxLength(15);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_Customers");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(5)
                .IsFixedLength();
            entity.Property(e => e.City).HasMaxLength(15);
            entity.Property(e => e.CompanyName).HasMaxLength(40);
            entity.Property(e => e.ContactName).HasMaxLength(30);
            entity.Property(e => e.Country).HasMaxLength(15);
        });

        modelBuilder.Entity<CustomerSetting>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_dbo.CustomerSetting");

            entity.ToTable("CustomerSetting");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(5)
                .IsFixedLength();
            entity.Property(e => e.Setting).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithOne(p => p.CustomerSetting)
                .HasForeignKey<CustomerSetting>(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerSetting_Customer");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK_dbo.Employee");

            entity.ToTable("Employee");

            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.City).HasMaxLength(15);
            entity.Property(e => e.Country).HasMaxLength(15);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.HireDate).HasColumnType("datetime");
            entity.Property(e => e.LastName).HasMaxLength(20);

            entity.HasMany(d => d.Territories).WithMany(p => p.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeTerritory",
                    r => r.HasOne<Territory>().WithMany()
                        .HasForeignKey("TerritoryId")
                        .HasConstraintName("FK_dbo.EmployeeTerritories_dbo.Territory_TerritoryId"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("FK_dbo.EmployeeTerritories_dbo.Employee_EmployeeId"),
                    j =>
                    {
                        j.HasKey("EmployeeId", "TerritoryId").HasName("PK_dbo.EmployeeTerritories");
                    });
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_Orders");

            entity.ToTable("Order");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(5)
                .IsFixedLength();
            entity.Property(e => e.Freight)
                .HasDefaultValueSql("((0))")
                .HasColumnType("money");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.ShippedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Orders_Customers");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK_OrderDetails");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Details_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Details_Products");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK_Products");

            entity.ToTable("Product");

            entity.Property(e => e.ProductName).HasMaxLength(40);
            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.UnitPrice)
                .HasDefaultValueSql("((0))")
                .HasColumnType("money");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Products_Categories");
        });

        modelBuilder.Entity<Territory>(entity =>
        {
            entity.HasKey(e => e.TerritoryId).HasName("PK_dbo.Territory");

            entity.ToTable("Territory");

            entity.Property(e => e.TerritoryId).HasMaxLength(20);
            entity.Property(e => e.TerritoryDescription).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
