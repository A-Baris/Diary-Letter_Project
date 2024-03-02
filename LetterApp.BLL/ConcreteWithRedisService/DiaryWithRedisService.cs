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
    public class DiaryWithRedisService : RepositoryLetterAppWithRedis<Diary>, IDiaryWithRedisService
    {
        public DiaryWithRedisService(IRedis_Cache<Diary> redis, IRepositoryLetterApp<Diary> repository) : base(redis, repository)
        {
        }
    }
}
