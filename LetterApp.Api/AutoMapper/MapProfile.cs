using AutoMapper;
using LetterApp.Api.DTOs;
using LetterApp.Api.DTOs.Diary;
using LetterApp.Api.DTOs.DiaryNote;
using LetterApp.Entity.Entities;

namespace LetterApp.Api.AutoMapper
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<Diary, DiaryDTO>().ReverseMap();
            CreateMap<Diary, DiaryEditDTO>().ReverseMap();

            CreateMap<DiaryNote,DiaryNoteCreateDTO>().ReverseMap();

            CreateMap<DiaryNote,DiaryNoteEditDTO>().ReverseMap();

        }
        
    }
}
