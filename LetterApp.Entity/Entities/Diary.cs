using LetterApp.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterApp.Entity.Entities
{
    public class Diary:BaseClass
    {
   
        [MaxLength(100)]
        public string Header { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public virtual IEnumerable<DiaryNote>? DiaryNotes { get; set; }
    }
}
