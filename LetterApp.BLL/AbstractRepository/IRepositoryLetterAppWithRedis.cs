using LetterApp.Entity.BaseEntity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterApp.BLL.AbstractRepository
{
    public interface IRepositoryLetterAppWithRedis<T> where T : BaseClass
    {
        Task<T> Create(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
      
        Task<T> Update(T entity,int id);
      
        Task<bool> Delete(int id);
    

        
    }
}
