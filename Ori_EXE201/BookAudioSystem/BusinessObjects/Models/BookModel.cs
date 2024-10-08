using BookAudioSystem.BusinessObjects.Entities;

namespace BookAudioSystem.BusinessObjects.Models
{
    public class BookModel
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }

        public List<string> Tags { get; set; }

    }
}
