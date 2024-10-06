using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookAudioSystem.BusinessObjects.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Ensure that UserID is auto-generated
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        public DateTime? birthDate;
        
        public string IdentityCard { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string Province { get; set; }

        public DateTime createdDate;
        
        public string Token { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankName { get; set; }

        // Navigation properties
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public Wallet Wallet { get; set; }
    }
}
