using LetterApp.BLL.AbstractRepository;
using LetterApp.Dal.ProjectContext;
using LetterApp.Entity.BaseEntity;
using LetterApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterApp.BLL.Repository
{
    public class RepositoryLetterApp<T> : IRepositoryLetterApp<T> where T : BaseClass
    {
        private readonly LetterAppContext _context;
        private DbSet<T> _entity;


        public RepositoryLetterApp(LetterAppContext context)
        {
            _context = context;
          _entity = context.Set<T>();
        }
        public async Task<T> Create(T entity)
        {
            try
            {
                 await _entity.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> Delete(int id)
        {

            try
            {
                var entity = await GetById(id);
                if(entity != null)
                {
                     _entity.Remove(entity);
                     await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await _entity.Where(x => x.Status == Entity.Enums.BaseStatus.Active).ToListAsync();
                
            }
            catch(Exception ex) {
                throw;
            
            }
        }

        public async Task<T> GetById(int id)
        {
            try
            {
                if(id!=null || id != 0)
                {
                    return await _entity.FindAsync(id);
                }
                return null;
                
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<T> Update(T entity)
        {
            try
            {
               

              
                     entity.Status = Entity.Enums.BaseStatus.Updated;
                     _context.Set<T>().Entry(entity).State = EntityState.Modified;
                     _context.SaveChanges();
                    return entity;
               
               
               
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
