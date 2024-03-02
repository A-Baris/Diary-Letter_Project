namespace LetterApp.Api.DTOs.Diary
{
    public class DiaryEditDTO
    {
        public int Id { get; set; }
        public string Header { get; set; }

        public string? Description { get; set; }
        public int UserId { get; set; }
    }
}
