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
        public DbSet<Entities.ParameterType> ParameterTypes { get; set; }
        public DbSet<Entities.Parameter> Parameters { get; set; }


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

            #region seed

            modelBuilder.Entity<ApplicationUser>().HasData(

           CreateUser(Guid.Parse("402e9a22-8b21-11ea-bc55-0242ac130003"),
           "Korkmaz Ltd.",
           "Mustafa Korkmaz",
           "mustafakorkmazdev@gmail.com"));

            ApplicationUser CreateUser(Guid id, string title, string name, string email)
            {
                return new ApplicationUser
                {
                    Id = id,
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    EmailConfirmed = true,
                    CreatedAt = new DateTime(2020, 4, 30),
                    UserName = email,
                    NormalizedUserName = email.ToUpper(),
                    SecurityStamp = "951a4c00-20d0-4d65-9d4a-7db4001c834c",
                    ConcurrencyStamp = "024e1046-752c-4943-9373-5ac78ab5601a",
                    PasswordHash = "AD5bszN5VbOZSQW+1qcXQb08ElGNt9uNoTrsNenNHSsD1g2Gp6ya4+uFJWmoUsmfng==",
                    Title = title,
                    NameSurname = name
                };
            }

            modelBuilder.Entity<Entities.Customer>().HasData(
                  CreateCustomer(1,
                  Guid.Parse("402e9a22-8b21-11ea-bc55-0242ac130003"),
                    "Akcam Ltd. ",
                    "Esra Korkmaz"));

            Entities.Customer CreateCustomer(int id, Guid userId, string title, string person)
            {
                return new Entities.Customer
                {
                    AuthorizedPersonName = person,
                    Id = id,
                    CreatedAt = new DateTime(2020, 4, 30),
                    Title = title,
                    UserId = userId,
                    RemainingBalance = 0
                };
            }

            modelBuilder.Entity<Entities.ParameterType>().HasData(
                new Entities.ParameterType
                {
                    Id = 1,
                    Name = "CustomerOperationType"
                });

            modelBuilder.Entity<Entities.Parameter>().HasData(
                CreateParameter(1,
                  Guid.Parse("402e9a22-8b21-11ea-bc55-0242ac130003"),
                    1,
                    "Diğer"));

            Entities.Parameter CreateParameter(int id, Guid userId, int typeId, string name)
            {
                return new Entities.Parameter
                {
                    Id = id,
                    UserId = userId,
                    Name = name,
                    ParameterTypeId = typeId
                };
            }

            #endregion seed
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