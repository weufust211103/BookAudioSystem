namespace BookAudioSystem.BusinessObjects.Entities
{
    public class Tag
    {
        public int TagID { get; set; }
        public string TagName { get; set; }

        // Navigation property
        public ICollection<BookTag> BookTags { get; set; }
    }
}
