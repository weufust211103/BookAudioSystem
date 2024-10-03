namespace BookAudioSystem.BusinessObjects.Entities
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int BookID { get; set; }
        public int UserID { get; set; }
        public int OwnerID { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        // Navigation properties
        public Book Book { get; set; }
        public User User { get; set; }
        public User Owner { get; set; }
    }
}
