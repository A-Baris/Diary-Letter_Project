using LetterApp.BLL.AbstractRepository;
using LetterApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterApp.BLL.AbstractWithRedisServices
{
    public interface IUserWithRedisService:IRepositoryLetterAppWithRedis<User>
    {
    }
}
