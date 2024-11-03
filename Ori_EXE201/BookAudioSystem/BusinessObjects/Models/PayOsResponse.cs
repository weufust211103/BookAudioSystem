namespace BookAudioSystem.BusinessObjects.Models
{
    public class PayOsResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public ICollection<object>? Data { get; set; }
    }

}
