using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dal.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace Dal.Db
{
    public class BlackCoveredLedgerDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim<Guid>,
              ApplicationUserRole<Guid>, ApplicationUserLogin<Guid>, ApplicationRoleClaim<Guid>, ApplicationUserToken<Guid>>
    {
        public BlackCoveredLedgerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entities.Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            #region identity user modifications

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.ConcurrencyStamp)
                .HasColumnType("varchar(36)")
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.Email)
                .HasColumnType("varchar(50)")
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.NormalizedEmail)
                .HasColumnType("varchar(50)")
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
               .Property(p => p.UserName)
               .HasColumnType("varchar(50)")
               .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.NormalizedUserName)
                .HasColumnType("varchar(50)")
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.PasswordHash)
                .HasColumnType("varchar(8000)")
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.PhoneNumber)
                .HasColumnType("varchar(12)");

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.SecurityStamp)
                .HasColumnType("varchar(100)")
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.NameSurname)
                .HasColumnType("varchar(100)");

            #endregion identity user modifications

            #region role modifications

            modelBuilder.Entity<ApplicationRole>()
                .Property(p => p.ConcurrencyStamp)
                .HasColumnType("varchar(36)")
                .IsRequired();

            modelBuilder.Entity<ApplicationRole>()
                .Property(p => p.Name)
                .HasColumnType("varchar(20)")
                .IsRequired();

            modelBuilder.Entity<ApplicationRole>()
                .Property(p => p.NormalizedName)
                .HasColumnType("varchar(20)")
                .IsRequired();

            #endregion role modifications


            #region user login modifications

            modelBuilder.Entity<ApplicationUserLogin<Guid>>()
                .Property(p => p.LoginProvider)
                .HasColumnType("varchar(50)")
                  .IsRequired();

            modelBuilder.Entity<ApplicationUserLogin<Guid>>()
                .Property(p => p.ProviderKey)
                .HasColumnType("varchar(128)")
                  .IsRequired();

            modelBuilder.Entity<ApplicationUserLogin<Guid>>()
                .Property(p => p.ProviderDisplayName)
                .HasColumnType("varchar(100)");


            #endregion user login modifications

            #region user token modifications

            modelBuilder.Entity<ApplicationUserToken<Guid>>()
                .Property(p => p.LoginProvider)
                .HasColumnType("varchar(50)")
                  .IsRequired();

            modelBuilder.Entity<ApplicationUserToken<Guid>>()
                .Property(p => p.Name)
                .HasColumnType("varchar(50)")
                  .IsRequired();

            modelBuilder.Entity<ApplicationUserToken<Guid>>()
                .Property(p => p.Value)
                .HasColumnType("varchar(500)")
                  .IsRequired();

            #endregion user token modifications

            #region user claim modifications

            modelBuilder.Entity<ApplicationUserClaim<Guid>>()
                .Property(p => p.ClaimType)
                .HasColumnType("varchar(50)")
                  .IsRequired();

            modelBuilder.Entity<ApplicationUserClaim<Guid>>()
                .Property(p => p.ClaimValue)
                .HasColumnType("varchar(50)")
                  .IsRequired();

            #endregion user claim modifications

            #region role claim modifications

            modelBuilder.Entity<ApplicationRoleClaim<Guid>>()
                .Property(p => p.ClaimType)
                .HasColumnType("varchar(50)")
                  .IsRequired();

            modelBuilder.Entity<ApplicationRoleClaim<Guid>>()
                .Property(p => p.ClaimValue)
                .HasColumnType("varchar(50)")
                  .IsRequired();

            #endregion role claim modifications

            //#region dbo.userLogs

            //modelBuilder.Entity<UserLog>()
            //    .HasIndex(b => b.UserId)
            //    .HasName("IX_UserId");

            //#endregion #dbo.userLogs

            //avoid nvarchar (use varchar)
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                   .Where(t => t.ClrType != typeof(ApplicationUser)
                    && t.ClrType != typeof(ApplicationUserLogin<string>)
                    && t.ClrType != typeof(ApplicationUserRole<string>)
                    && t.ClrType != typeof(ApplicationUserClaim<string>)
                    && t.ClrType != typeof(ApplicationRoleClaim<string>)
                    && t.ClrType != typeof(ApplicationUserToken<string>)
                    && t.ClrType != typeof(ApplicationRole))
                   .SelectMany(t => t.GetProperties())
                   .Where(p => p.ClrType == typeof(string)))
            {
                var length = property.GetMaxLength();

                property.SetColumnType(length == null ? "varchar" : string.Format("varchar({0})", length));
            }

        }

        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                                 || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                                 || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}