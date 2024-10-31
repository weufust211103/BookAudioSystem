using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookAudioSystem.BusinessObjects.Entities
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }

        // Navigation properties
        public User User { get; set; }
        public ICollection<BookTag> BookTags { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
