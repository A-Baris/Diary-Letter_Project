namespace LetterApp.WEB.Models
{
    public class UserUpdate
    {
        public int? Id { get; set; }
        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? PasswordConfirmed { get; set; }
    }
}
