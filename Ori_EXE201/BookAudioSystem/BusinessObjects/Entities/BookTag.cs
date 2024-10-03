namespace BookAudioSystem.BusinessObjects.Entities
{
    public class BookTag
    {
        public int BookID { get; set; }
        public int TagID { get; set; }

        // Navigation properties
        public Book Book { get; set; }
        public Tag Tag { get; set; }
    }
}
