using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookAudioSystem.BusinessObjects.Entities
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }
        public int BookID { get; set; }
        public int UserID { get; set; }
        public int OwnerID { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? SoldDate { get; set; } // New field to track the sale date

        // Navigation properties
        public Book Book { get; set; }
        public User User { get; set; }
        public User Owner { get; set; }
    }
}
