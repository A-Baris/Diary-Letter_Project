namespace LetterApp.WEB.Models.View_Models.DiaryVM
{
    public class DiaryEditVM
    {
        public int Id { get; set; }
        public string Header { get; set; }

        public string? Description { get; set; }
        public int UserId { get; set; }
    }
}
