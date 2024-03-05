using LetterApp.Entity.Entities;
using System.ComponentModel.DataAnnotations;

namespace LetterApp.Api.DTOs.User
{
    public class UserCreateDTO
    {
        public string IdentityId { get; set; }
     
        public string Username { get; set; }
    
        public string Email { get; set; }
  
        public string Password { get; set; }
     
    }
}
