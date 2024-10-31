﻿// <auto-generated />
using System;
using BookAudioSystem.BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookAudioSystem.Migrations
{
    [DbContext(typeof(RentalBookDbContext))]
    partial class RentalBookDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.Book", b =>
                {
                    b.Property<int>("BookID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BookID"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("BookID");

                    b.HasIndex("UserID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.BookTag", b =>
                {
                    b.Property<int>("BookID")
                        .HasColumnType("integer");

                    b.Property<int>("TagID")
                        .HasColumnType("integer");

                    b.HasKey("BookID", "TagID");

                    b.HasIndex("TagID");

                    b.ToTable("BookTags");
                });

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderID"));

                    b.Property<int>("BookID")
                        .HasColumnType("integer");

                    b.Property<int>("BuyerID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("OrderID");

                    b.HasIndex("BookID");

                    b.HasIndex("BuyerID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleID"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleID = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleID = 2,
                            RoleName = "Owner"
                        },
                        new
                        {
                            RoleID = 3,
                            RoleName = "Renter"
                        });
                });

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.Tag", b =>
                {
                    b.Property<int>("TagID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TagID"));

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TagID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.Transaction", b =>
                {
                    b.Property<int>("TransactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TransactionID"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int>("BookID")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("BorrowDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("OwnerID")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("SoldDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("TransactionID");

                    b.HasIndex("BookID");

                    b.HasIndex("OwnerID");

                    b.HasIndex("UserID");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BankAccountNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IdentityCard")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Ward")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("birthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("UserID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            Address = "123 Admin Street, Admin City",
                            BankAccountNumber = "1234567890",
                            BankName = "Admin Bank",
                            District = "Admin District",
                            Email = "superadmin@rentalbook.com",
                            FullName = "Super Admin",
                            IdentityCard = "123456789",
                            Password = "$2a$11$K7kKKUCQeNGm7leZpSDr6u4Af/ENMThcseG73dovI60Q72SA28Z0e",
                            PhoneNumber = "+1234567890",
                            Province = "Admin Province",
                            Token = "default_token",
                            Ward = "Admin Ward",
                            birthDate = new DateTime(1979, 12, 31, 17, 0, 0, 0, DateTimeKind.Utc),
                            createdDate = new DateTime(2024, 10, 30, 15, 40, 34, 363, DateTimeKind.Utc).AddTicks(9947)
                        });
                });

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.UserRole", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.Property<int>("RoleID")
                        .HasColumnType("integer");

                    b.HasKey("UserID", "RoleID");

                    b.HasIndex("RoleID");

                    b.ToTable("UsersRoles");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            RoleID = 1
                        });
                });

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.Wallet", b =>
                {
                    b.Property<int>("WalletID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("WalletID"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("WalletID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.Book", b =>
                {
                    b.HasOne("BookAudioSystem.BusinessObjects.Entities.User", "User")
                        .WithMany("Books")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.BookTag", b =>
                {
                    b.HasOne("BookAudioSystem.BusinessObjects.Entities.Book", "Book")
                        .WithMany("BookTags")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookAudioSystem.BusinessObjects.Entities.Tag", "Tag")
                        .WithMany("BookTags")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.Order", b =>
                {
                    b.HasOne("BookAudioSystem.BusinessObjects.Entities.Book", "Book")
                        .WithMany("Orders")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookAudioSystem.BusinessObjects.Entities.User", "Buyer")
                        .WithMany("Orders")
                        .HasForeignKey("BuyerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Buyer");
                });

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.Transaction", b =>
                {
                    b.HasOne("BookAudioSystem.BusinessObjects.Entities.Book", "Book")
                        .WithMany("Transactions")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookAudioSystem.BusinessObjects.Entities.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BookAudioSystem.BusinessObjects.Entities.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Owner");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.UserRole", b =>
                {
                    b.HasOne("BookAudioSystem.BusinessObjects.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookAudioSystem.BusinessObjects.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.Wallet", b =>
                {
                    b.HasOne("BookAudioSystem.BusinessObjects.Entities.User", "User")
                        .WithOne("Wallet")
                        .HasForeignKey("BookAudioSystem.BusinessObjects.Entities.Wallet", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.Book", b =>
                {
                    b.Navigation("BookTags");

                    b.Navigation("Orders");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.Tag", b =>
                {
                    b.Navigation("BookTags");
                });

            modelBuilder.Entity("BookAudioSystem.BusinessObjects.Entities.User", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("Orders");

                    b.Navigation("Transactions");

                    b.Navigation("UserRoles");

                    b.Navigation("Wallet")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
