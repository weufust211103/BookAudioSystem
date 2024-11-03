namespace BookAudioSystem.BusinessObjects.Models
{
    public class OrderDetailDto
    {
        public int OrderID { get; set; }
        public int BookID { get; set; }
        public string OrderStatus { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
        public BuyerInfoDto Buyer { get; set; }
    }

}
