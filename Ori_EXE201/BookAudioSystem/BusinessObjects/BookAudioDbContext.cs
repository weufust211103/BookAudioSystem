using Microsoft.EntityFrameworkCore;
using BookAudioSystem.BusinessObjects.Entities;
using System.Text;
using System.Security.Cryptography;
namespace BookAudioSystem.BusinessObjects
{
    public class BookAudioDbContext: DbContext
    {
        public BookAudioDbContext(DbContextOptions<BookAudioDbContext> options) : base(options) { }

        // Define DbSets for your entities (tables)
        public DbSet<Book> Books { get; set; }
        public DbSet<Audio> Audios { get; set; }
        public DbSet<User> Users { get; set; }

        // Optionally override OnModelCreating if you need to configure relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API configurations
            modelBuilder.Entity<Book>()
                        .HasMany(b => b.Audios)
                        .WithOne(a => a.Book)
                        .HasForeignKey(a => a.BookId);

            // Additional configurations can go here
            // Hash the password (example with SHA256, you should use a stronger hash and salt)
            string password = "SuperAdminPassword";
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            

                // Seed the SuperAdmin
                modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        UserId = 1, // Ensure this Id doesn't conflict with existing data
                        Username = "superadmin",
                        Email = "superadmin@example.com",
                        PasswordHash = passwordHash, // Store the hashed password
                        Role = "SuperAdmin"
                    });
            
        }
    }
}
