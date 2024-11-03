using BookAudioSystem.BusinessObjects.Entities;

namespace BookAudioSystem.BusinessObjects.Models
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public int BookID { get; set; }
        public int BuyerID { get; set; }
        public string OrderStatus { get; set; } 
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
