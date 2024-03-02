using LetterApp.Entity.Entities;

namespace LetterApp.Api.DTOs
{
    public class DiaryWithNoteDTO
    {
        public int DiaryId { get; set; }
        public int DiaryNoteId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string DiaryHeader { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
