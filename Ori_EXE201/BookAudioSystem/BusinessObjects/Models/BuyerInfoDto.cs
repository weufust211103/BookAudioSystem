namespace BookAudioSystem.BusinessObjects.Models
{
    public class BuyerInfoDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
    }
}
