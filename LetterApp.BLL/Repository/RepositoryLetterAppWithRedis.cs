using LetterApp.BLL.AbstractRepository;
using LetterApp.BLL.Redis.Abstracts;
using LetterApp.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LetterApp.BLL.Repository
{
    public class RepositoryLetterAppWithRedis<T> : IRepositoryLetterAppWithRedis<T> where T : BaseClass
    {
        private readonly IRedis_Cache<T> _redis;
        private readonly IRepositoryLetterApp<T> _repository;

        public RepositoryLetterAppWithRedis(IRedis_Cache<T> redis,IRepositoryLetterApp<T> repository)
        {
           _redis = redis;
          _repository = repository;
        }
        public async Task<T> Create(T entity)
        {
            try
            {
                await _repository.Create(entity);
                await _redis.SetEntity(entity);
                return entity;
            }
            catch (Exception ex)
            {
                    Console.WriteLine($"An error occurred during creating the entity: {ex.Message}");

                    throw new Exception("An error occurred during creating the entity.",ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var resultRepo = await _repository.Delete(id);
                var resultCache= await _redis.Delete(id);
                if (resultRepo == true && resultCache == true) return true;
                return false;

            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred during deleting the entity: {ex.Message}");

                throw new Exception("An error occurred during deleting the entity.", ex);
            }
        }
     
        public Task<IEnumerable<T>> GetAll()
        {
            try
            {
               
                var cacheEntities = _redis.GetAllEntities();
                if(cacheEntities!=null) return cacheEntities;
                return _repository.GetAll();

            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred during Get the entities: {ex.Message}");

                throw new Exception("An error occurred during Get the entities.", ex);
            }
        }

        public async Task<T> GetById(int id)
        {
            try
            {
                var cacheEntity = await _redis.GetEntityById(id);
                if (cacheEntity != null) return cacheEntity;
                return await _repository.GetById(id) ?? null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during Get the entity: {ex.Message}");

                throw new Exception("An error occurred during Get the entity.", ex);
            }
        }
     

        public async Task<T> Update(T entity,int Id)
        {
            try
            {
             

                var result = await _repository.Update(entity);
                if (result == null)
                {
                    return null;
                }

                await _redis.Update(Id, entity);
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during updating the entity: {ex.Message}");

                throw new Exception("An error occurred during updating the entity.", ex);
            }
        }
       
    }
}
