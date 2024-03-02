using System.Web.Mvc;

namespace LetterApp.WEB.Models.View_Models.DiaryNoteVM
{
    public class DiaryNoteCreateVM
    {
        public int DiaryId { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Body { get; set; }

        public string? ReceiverEmail { get; set; }
    }
}
