using LetterApp.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterApp.Entity.Entities
{
    public class User : BaseClass
    {
        [MaxLength(20)]
        public string Username { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(255)]
        public string Password { get; set; }
        [MaxLength(255)]
        public string PasswordConfirmed { get; set; }
        public virtual UserProfile? Profile { get; set; }
        public  ICollection<Diary>? Diaries { get; set; }
    }
}
