namespace BookAudioSystem.BusinessObjects.Entities
{
    public class Audio
    {
        public int AudioId { get; set; }
        public string FilePath { get; set; }
        public TimeSpan Duration { get; set; }

        // Foreign key
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
