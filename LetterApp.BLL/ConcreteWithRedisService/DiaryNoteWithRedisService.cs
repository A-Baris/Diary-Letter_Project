using LetterApp.BLL.AbstractRepository;
using LetterApp.BLL.AbstractWithRedisServices;
using LetterApp.BLL.Redis.Abstracts;
using LetterApp.BLL.Repository;
using LetterApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterApp.BLL.ConcreteWithRedisService
{
    public class DiaryNoteWithRedisService : RepositoryLetterAppWithRedis<DiaryNote>, IDiaryNoteWithRedisService
    {
        public DiaryNoteWithRedisService(IRedis_Cache<DiaryNote> redis, IRepositoryLetterApp<DiaryNote> repository) : base(redis, repository)
        {
        }
    }
}
