using System.ComponentModel.DataAnnotations;

namespace LetterApp.Api.DTOs.DiaryNote
{
    public class DiaryNoteCreateDTO
    {
        public int DiaryId { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }
       
        public string? ReceiverEmail { get; set; }
    }
}
