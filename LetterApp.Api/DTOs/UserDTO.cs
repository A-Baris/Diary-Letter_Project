using LetterApp.Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace LetterApp.Api.DTOs
{
    public class UserDTO
    {
    
        public int Id { get; set; }
       
        public string Username { get; set; }
       
        public string Email { get; set; }
      
        public string Password { get; set; }
      
        public string PasswordConfirmed { get; set; }
       
    }
}
