namespace BookAudioSystem.BusinessObjects.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }

        // Navigation property
        public ICollection<Audio> Audios { get; set; }
    }
}
