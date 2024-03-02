
using LetterApp.WEB.Models.View_Models.DiaryNoteVM;
using LetterApp.WEB.Models.View_Models.DiaryVM;

namespace LetterApp.WEB.Models.View_Models
{
    public class DiaryWithNoteVM
    {
        public int DiaryId { get; set; }
        public int DiaryNoteId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string DiaryHeader { get; set; }
        public DateTime? CreatedDate { get; set; }


    }
}

