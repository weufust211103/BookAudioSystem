using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookAudioSystem.BusinessObjects.Entities
{
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagID { get; set; }
        public string TagName { get; set; }

        // Navigation property
        public ICollection<BookTag> BookTags { get; set; }
    }
}
