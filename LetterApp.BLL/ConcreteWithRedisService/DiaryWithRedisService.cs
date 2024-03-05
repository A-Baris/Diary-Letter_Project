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
        private readonly IRedis_Cache<DiaryNote> _redisDiaryNoteCache;

        public DiaryWithRedisService(IRedis_Cache<Diary> redis, IRepositoryLetterApp<Diary> repository, IRedis_Cache<DiaryNote> redisDiaryNoteCache) : base(redis, repository)
        {
            _redisDiaryNoteCache = redisDiaryNoteCache;
        }
        public async Task<bool> DeleteWithRelationEntity(int id)
        {
            await Delete(id);
            var notes = await _redisDiaryNoteCache.GetAllEntities();
            foreach (var item in notes)
            {
                if (item.DiaryId == id)
                {
                    await _redisDiaryNoteCache.Delete(item.Id);
                }
            }
            return true;
        }
    }
}
