using LetterApp.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterApp.Entity.Entities
{
    public class DiaryNote:BaseClass
    {
        public int DiaryId { get; set; }
        public Diary? Diary { get; set; }
        [MaxLength(150)]
        public string Title { get; set; }
        [MaxLength(3000)]
        public string Body { get; set; }
        [MaxLength(100)]
        public string? ReceiverEmail { get; set; }
    }
}
