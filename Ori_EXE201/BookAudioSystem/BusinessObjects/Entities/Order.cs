using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookAudioSystem.BusinessObjects.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        public int BookID { get; set; }
        public int BuyerID { get; set; }
        public string OrderStatus { get; set; } // e.g., "Buy", "Rent"
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }

        // Navigation properties
        public Book Book { get; set; }
        public User Buyer { get; set; }
    }
}
