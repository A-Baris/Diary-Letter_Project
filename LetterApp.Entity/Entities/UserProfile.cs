using LetterApp.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterApp.Entity.Entities
{
    public class UserProfile:BaseClass
    {
        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(40)]
        public  string Surname { get; set; }
        [MaxLength(11)]
        public  string? PhoneNumber { get; set; }
        [MaxLength(40)]
        public  string City { get; set; }
       
        public User? User { get; set; }
    }
}
