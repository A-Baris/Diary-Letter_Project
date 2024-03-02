

namespace LetterApp.WEB.Models.View_Models.DiaryVM
{
    public class DiaryVM
    {
        public int Id { get; set; }
        public string Header { get; set; }

        public string? Description { get; set; }
        public int UserId { get; set; }
        public List<View_Models.DiaryNoteVM.DiaryNoteDetailVM> DiaryNotes { get; set; }
    }
}
