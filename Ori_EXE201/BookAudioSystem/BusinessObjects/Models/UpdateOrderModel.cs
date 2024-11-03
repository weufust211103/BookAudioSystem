 namespace BookAudioSystem.BusinessObjects.Models
{
    public class UpdateOrderModel
    {
        public int OrderId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
    }

}
