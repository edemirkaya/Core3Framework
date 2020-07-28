using CommonCore.Interfaces;
using CommonCore.Server.Base;
using CommonCore.Server.Services;
using Core3_Framework.Contracts.DataContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Core3_Framework.Data
{
    public partial class AppDb : ContextBase
    {

        public AppDb(DbContextOptions<AppDb> options, UserResolverService userService) 
            : base(options)
        {
            AuditTables = this.AuditTables;

            base.userName = userService.GetUserName();
            base.userId = userService.GetUserId();
        }

        public AppDb(DbContextOptions<AppDb> options) : base(options)
        {
            AuditTables = this.AuditTables;
        }

        public virtual DbSet<CacheItems> CacheItem { get; set; }
        public virtual DbSet<Audits> Audit { get; set; }
        public virtual DbSet<AuditTables> AuditTable { get; set; }
        public virtual DbSet<Users> User { get; set; }
        public virtual DbSet<UserRoles> UserRole { get; set; }
        public virtual DbSet<Roles> Role { get; set; }

        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }

        protected override IAuditLogEntity GetAuditLogEntity()
        {
            return (IAuditLogEntity)new Audits();
        }


        protected override void AddAuditLogEntityToDbSet(IAuditLogEntity entity)
        {
            Audit.Add((Audits)entity);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<AppDb>();
            var connectionString = configuration.GetConnectionString("ConStr");
            //optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CacheItems>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Audits>(entity =>
            {
                entity.Property(e => e.ProcessType)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.ProcessTime).HasColumnType("timestamp");

                entity.Property(e => e.ProcessOwner)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.KeyValues)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<AuditTables>(entity =>
            {
                entity.HasKey(e => e.TableName);

                entity.Property(e => e.TableName)
                    .HasMaxLength(255)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(20);

                entity.Property(e => e.Username).HasMaxLength(20);
                entity.HasMany(m => m.UserRoles);
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("User_Role");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_User_Id");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_Role_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.User_Role_dbo.User_User_Id");

                entity.HasOne(d => d.Roles)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.User_Role_dbo.Role_Role_Id");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId);
                entity.Property(e => e.CategoryName).HasMaxLength(100);

                entity.HasMany(m => m.Products);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e=> e.ProductName).HasMaxLength(100);
                entity.Property(e => e.QuantityPerUnit);
                entity.Ignore(e => e.Category);
                entity.Property(e => e.UnitPrice).HasColumnType("decimal");
                entity.Property(e => e.UnitsInStock);
            });
        }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDb>
        {
            public AppDb CreateDbContext(string[] args)
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
                var builder = new DbContextOptionsBuilder<AppDb>();
                var connectionString = configuration.GetConnectionString("ConStr");
                //builder.sq(connectionString);
                return new AppDb(builder.Options);
            }
        }
    }
}
