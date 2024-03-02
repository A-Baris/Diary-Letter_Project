using LetterApp.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterApp.Entity.BaseEntity
{
    public class BaseClass
    {
        public BaseClass()
        {
            Status = BaseStatus.Active;
        }
        public int Id { get; set; }
        public BaseStatus Status { get; set; }
        [MaxLength(255)]
        public string? Created_Ip  { get; set; }
        [MaxLength(255)]
        public string? Created_MachineName { get; set; }
        public DateTime? Created_Date { get; set; }
        [MaxLength(255)]
        public string? Updated_Ip { get; set; }
        [MaxLength(255)]
        public string? Updated_MachineName { get; set; }
        public DateTime? Updated_Date { get; set; }

    }
}
