using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace API.Entities
{
    public partial class minidressContext : DbContext
    {
        public minidressContext()
        {
        }

        public minidressContext(DbContextOptions<minidressContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<AdminRefreshToken> AdminRefreshTokens { get; set; }
        public virtual DbSet<AdminRole> AdminRoles { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillDetail> BillDetails { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=minidress;User ID=sa;Password=Bach1604@@");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.HashId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("hash_id");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LockedCount).HasColumnName("locked_count");

                entity.Property(e => e.LockedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("locked_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("role");

                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("token");
            });

            modelBuilder.Entity<AdminRefreshToken>(entity =>
            {
                entity.HasKey(e => e.TokenId);

                entity.ToTable("admin_refresh_token");

                entity.Property(e => e.TokenId).HasColumnName("token_id");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiry_date");

                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.AdminRefreshTokens)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_admin_refresh_token_admin");
            });

            modelBuilder.Entity<AdminRole>(entity =>
            {
                entity.ToTable("admin_role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("role_name");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.AdminRoles)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Admin_Role_Admin_Id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AdminRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Admin_Role_Role_Id");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("bill");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.DateOrder)
                    .HasColumnType("datetime")
                    .HasColumnName("date_order");

                entity.Property(e => e.IdCustomer).HasColumnName("id_customer");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasColumnName("note");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bill_Customer");
            });

            modelBuilder.Entity<BillDetail>(entity =>
            {
                entity.ToTable("bill_detail");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.IdBill).HasColumnName("id_bill");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.IdBillNavigation)
                    .WithMany(p => p.BillDetails)
                    .HasForeignKey(d => d.IdBill)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillDetail_Bill");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.BillDetails)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillDetail_Product");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.HashId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("hash_id");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.LockedCount).HasColumnName("locked_count");

                entity.Property(e => e.LockedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("locked_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("token");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("image");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.Link)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("link");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImageProduct");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Color)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("color");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.IdType).HasColumnName("id_type");

                entity.Property(e => e.Material)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("material");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.New).HasColumnName("new");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductType");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("product_type");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("role_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
