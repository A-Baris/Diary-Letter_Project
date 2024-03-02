using LetterApp.Entity.Entities;
using System.ComponentModel.DataAnnotations;

namespace LetterApp.Api.DTOs.Diary
{
    public class DiaryDTO
    {

        public string Header { get; set; }

        public string? Description { get; set; }
        public int UserId { get; set; }


    }
}
