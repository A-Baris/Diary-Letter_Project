namespace LetterApp.Api.DTOs.DiaryNote
{
    public class DiaryNoteEditDTO
    {
        public int Id { get; set; }
        public int DiaryId { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }

        public string? ReceiverEmail { get; set; }
    }
}
