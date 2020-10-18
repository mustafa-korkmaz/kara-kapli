﻿// <auto-generated />
using System;
using Dal.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Dal.Migrations
{
    [DbContext(typeof(BlackCoveredLedgerDbContext))]
    [Migration("20201018111551_V5")]
    partial class V5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Dal.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AuthorizedPersonName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("DebtBalance")
                        .HasColumnType("double precision");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("character varying(12)")
                        .HasMaxLength(12);

                    b.Property<double>("ReceivableBalance")
                        .HasColumnType("double precision");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorizedPersonName = "Esra Korkmaz",
                            CreatedAt = new DateTime(2020, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DebtBalance = 0.0,
                            ReceivableBalance = 0.0,
                            Title = "Akcam Ltd. ",
                            UserId = new Guid("402e9a22-8b21-11ea-bc55-0242ac130003")
                        });
                });

            modelBuilder.Entity("Dal.Entities.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("character varying(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Dal.Entities.Identity.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7f9fcc26-c38c-46bd-86a7-b7b3d5959b78"),
                            ConcurrencyStamp = "f0e32c74-6510-484d-a6f4-db6a79eb82e4",
                            Name = "owner",
                            NormalizedName = "OWNER"
                        },
                        new
                        {
                            Id = new Guid("e964fe31-ba9a-4ee6-98c1-7fa84767868d"),
                            ConcurrencyStamp = "a620a23a-3760-4d4f-a095-79ffe5fd9a39",
                            Name = "admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("0967d456-60a8-43de-9ac8-5f15dfaa1909"),
                            ConcurrencyStamp = "7473ba90-3e20-491d-a4c2-ecbc7f22ec5f",
                            Name = "user",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = new Guid("8a158f67-b9aa-4dec-9e8f-53d29aeb1905"),
                            ConcurrencyStamp = "8a51070d-4ad9-4b7a-84af-c7b4bfb7aa41",
                            Name = "demo_user",
                            NormalizedName = "DEMO_USER"
                        });
                });

            modelBuilder.Entity("Dal.Entities.Identity.ApplicationRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ClaimValue")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Dal.Entities.Identity.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NameSurname")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(50);

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varchar(8000)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(12)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Settings")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<string>("Title")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("402e9a22-8b21-11ea-bc55-0242ac130003"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "024e1046-752c-4943-9373-5ac78ab5601a",
                            CreatedAt = new DateTime(2020, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mustafakorkmazdev@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NameSurname = "Mustafa Korkmaz",
                            NormalizedEmail = "MUSTAFAKORKMAZDEV@GMAIL.COM",
                            NormalizedUserName = "MUSTAFAKORKMAZDEV@GMAIL.COM",
                            PasswordHash = "AD5bszN5VbOZSQW+1qcXQb08ElGNt9uNoTrsNenNHSsD1g2Gp6ya4+uFJWmoUsmfng==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "951a4c00-20d0-4d65-9d4a-7db4001c834c",
                            Title = "Korkmaz Ltd.",
                            TwoFactorEnabled = false,
                            UserName = "mustafakorkmazdev@gmail.com"
                        });
                });

            modelBuilder.Entity("Dal.Entities.Identity.ApplicationUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ClaimValue")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Dal.Entities.Identity.ApplicationUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(128)");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Dal.Entities.Identity.ApplicationUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Dal.Entities.Identity.ApplicationUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Dal.Entities.Parameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<byte>("Order")
                        .HasColumnType("smallint");

                    b.Property<int>("ParameterTypeId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ParameterTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Parameters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            Name = "Cariye Borç",
                            Order = (byte)1,
                            ParameterTypeId = 2,
                            UserId = new Guid("402e9a22-8b21-11ea-bc55-0242ac130003")
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            Name = "Cariye Alacak",
                            Order = (byte)2,
                            ParameterTypeId = 1,
                            UserId = new Guid("402e9a22-8b21-11ea-bc55-0242ac130003")
                        },
                        new
                        {
                            Id = 3,
                            IsDeleted = false,
                            Name = "Tahsilat",
                            Order = (byte)3,
                            ParameterTypeId = 1,
                            UserId = new Guid("402e9a22-8b21-11ea-bc55-0242ac130003")
                        },
                        new
                        {
                            Id = 4,
                            IsDeleted = false,
                            Name = "Ödeme",
                            Order = (byte)4,
                            ParameterTypeId = 2,
                            UserId = new Guid("402e9a22-8b21-11ea-bc55-0242ac130003")
                        },
                        new
                        {
                            Id = 5,
                            IsDeleted = false,
                            Name = "Customer Debt",
                            Order = (byte)5,
                            ParameterTypeId = 2,
                            UserId = new Guid("402e9a22-8b21-11ea-bc55-0242ac130003")
                        },
                        new
                        {
                            Id = 6,
                            IsDeleted = false,
                            Name = "Customer Receivable",
                            Order = (byte)6,
                            ParameterTypeId = 1,
                            UserId = new Guid("402e9a22-8b21-11ea-bc55-0242ac130003")
                        },
                        new
                        {
                            Id = 7,
                            IsDeleted = false,
                            Name = "Collection",
                            Order = (byte)7,
                            ParameterTypeId = 1,
                            UserId = new Guid("402e9a22-8b21-11ea-bc55-0242ac130003")
                        },
                        new
                        {
                            Id = 8,
                            IsDeleted = false,
                            Name = "Payment",
                            Order = (byte)8,
                            ParameterTypeId = 2,
                            UserId = new Guid("402e9a22-8b21-11ea-bc55-0242ac130003")
                        });
                });

            modelBuilder.Entity("Dal.Entities.ParameterType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("ParameterTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "TransactionType.Receivable"
                        },
                        new
                        {
                            Id = 2,
                            Name = "TransactionType.Debt"
                        });
                });

            modelBuilder.Entity("Dal.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<bool>("IsDebt")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TypeId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Dal.Entities.Customer", b =>
                {
                    b.HasOne("Dal.Entities.Identity.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dal.Entities.Identity.ApplicationRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Dal.Entities.Identity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dal.Entities.Identity.ApplicationUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Dal.Entities.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dal.Entities.Identity.ApplicationUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Dal.Entities.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dal.Entities.Identity.ApplicationUserRole<System.Guid>", b =>
                {
                    b.HasOne("Dal.Entities.Identity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dal.Entities.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dal.Entities.Identity.ApplicationUserToken<System.Guid>", b =>
                {
                    b.HasOne("Dal.Entities.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dal.Entities.Parameter", b =>
                {
                    b.HasOne("Dal.Entities.ParameterType", "ParameterType")
                        .WithMany()
                        .HasForeignKey("ParameterTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dal.Entities.Identity.ApplicationUser", "User")
                        .WithMany("Parameters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dal.Entities.Transaction", b =>
                {
                    b.HasOne("Dal.Entities.Customer", "Customer")
                        .WithMany("Transactions")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dal.Entities.Parameter", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
