using LetterApp.WEB.Models.View_Models.DiaryVM;
using System.ComponentModel.DataAnnotations;

namespace LetterApp.WEB.Models.View_Models.DiaryNoteVM
{
    public class DiaryNoteDetailVM
    {
        public int Id { get; set; }
        public int DiaryId { get; set; }    
        public string Title { get; set; }
   
        public string Body { get; set; }
   
        public string? ReceiverEmail { get; set; }
    }
}
