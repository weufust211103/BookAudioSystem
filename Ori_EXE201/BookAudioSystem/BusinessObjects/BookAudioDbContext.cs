using Microsoft.EntityFrameworkCore;
using BookAudioSystem.BusinessObjects.Entities;
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
        }
    }
}
