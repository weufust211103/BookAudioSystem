using BookAudioSystem.BusinessObjects.Entities;
using BookAudioSystem.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace BookAudioSystem.BusinessObjects
{
    public class RentalBookDbContext : DbContext
    {
        public RentalBookDbContext(DbContextOptions<RentalBookDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UsersRoles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BookTag> BookTags { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ensure DateTime fields are stored as UTC in PostgreSQL
            modelBuilder.Entity<User>()
                .Property(u => u.birthDate)
                .HasConversion(
                    v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null,
                    v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null
                );

            modelBuilder.Entity<User>()
                .Property(u => u.createdDate)
                .HasConversion(
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                );

            // Configure primary keys for junction tables
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserID, ur.RoleID });
            modelBuilder.Entity<BookTag>().HasKey(bt => new { bt.BookID, bt.TagID });

            // Configure relationships
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserID);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleID);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.User)
                .WithMany(u => u.Books)
                .HasForeignKey(b => b.UserID);

            modelBuilder.Entity<BookTag>()
                .HasOne(bt => bt.Book)
                .WithMany(b => b.BookTags)
                .HasForeignKey(bt => bt.BookID);

            modelBuilder.Entity<BookTag>()
                .HasOne(bt => bt.Tag)
                .WithMany(t => t.BookTags)
                .HasForeignKey(bt => bt.TagID);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Book)
                .WithMany(b => b.Transactions)
                .HasForeignKey(t => t.BookID);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Owner)
                .WithMany()
                .HasForeignKey(t => t.OwnerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.User)
                .WithOne(u => u.Wallet)
                .HasForeignKey<Wallet>(w => w.UserID);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Book)
                .WithMany(b => b.Orders) // Book has a collection of Orders
                .HasForeignKey(o => o.BookID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Buyer)
                .WithMany(u => u.Orders) // User (Buyer) has a collection of Orders
                .HasForeignKey(o => o.BuyerID);
            // Seed data
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleID = 1, RoleName = "Admin" },
                new Role { RoleID = 2, RoleName = "Owner" },
                new Role { RoleID = 3, RoleName = "Renter" }
            );

            // Seed Super Admin with all fields
            var superAdminPassword = PasswordHasher.HashPassword("SuperAdminPassword123!");
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserID = 1,
                    Email = "superadmin@rentalbook.com",
                    Password = superAdminPassword,
                    FullName = "Super Admin",
                    birthDate = new DateTime(1980, 1, 1).ToUniversalTime(),  // Ensure DateTime is UTC
                    IdentityCard = "123456789",
                    PhoneNumber = "+1234567890",
                    Address = "123 Admin Street, Admin City",
                    Ward = "Admin Ward",
                    District = "Admin District",
                    Province = "Admin Province",
                    createdDate = DateTime.UtcNow,  // Ensure CreatedDate is in UTC
                    Token = "default_token",
                    BankAccountNumber = "1234567890",
                    BankName = "Admin Bank"
                }
            );

            // Assign Admin role to Super Admin
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserID = 1, RoleID = 1 }
            );
        }
    }

        
}