using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LetterApp.WEB.Models
{
    public class UserRegister
    {
     
        public string? Username { get; set; }
    
        public string? Email { get; set; }
  
        public string? Password { get; set; }
        
        public string? PasswordConfirmed { get; set; }
    }
}
