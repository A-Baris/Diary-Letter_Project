using LetterApp.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterApp.BLL.Redis.Abstracts
{
    public interface IRedis_Cache<T> where T : BaseClass
    {
        Task<T> SetEntity(T entity);
        Task<T> GetEntityById(int id);

        Task<IEnumerable<T>> GetAllEntities();
        Task<bool> Delete(int id);

        Task<T> Update(int updatedId, T newEntity);
 
    }
}
