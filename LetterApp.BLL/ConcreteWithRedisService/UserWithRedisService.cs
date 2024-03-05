using LetterApp.BLL.AbsractService;
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
    public class UserWithRedisService : RepositoryLetterAppWithRedis<User>, IUserWithRedisService
    {
        public UserWithRedisService(IRedis_Cache<User> redis, IRepositoryLetterApp<User> repository) : base(redis, repository)
        {
        }

       
    }
}
