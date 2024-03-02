using LetterApp.BLL.AbsractService;
using LetterApp.BLL.AbsractServices;
using LetterApp.BLL.AbstractRepository;
using LetterApp.BLL.AbstractWithRedisServices;
using LetterApp.BLL.ConcreteWithRedisService;
using LetterApp.BLL.FluentValidationServices;
using LetterApp.BLL.Redis.Concretes;
using LetterApp.BLL.Repository;
using LetterApp.BLL.Services;
using LetterApp.Entity.Entities;
using LetterApp.Entity.RedisConfigs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterApp.IOC.Container
{
    public class ServiceConfig
    {
        public static void ServiceConfiguration(IServiceCollection services, IConfiguration configuration)
        {
           services.AddTransient(typeof(IRepositoryLetterApp<>), typeof(RepositoryLetterApp<>));
           services.AddTransient(typeof(IRepositoryLetterAppWithRedis<>), typeof(RepositoryLetterAppWithRedis<>));
           services.AddScoped<IUserService, UserService>();
           services.AddScoped<IDiaryService, DiaryService>();

            services.AddScoped<IUserWithRedisService, UserWithRedisService>(sp =>
            {
                var dbNo = RedisDatabase.Users;
                var entityKey = RedisEntityKey.UserKey;
                var url = configuration["CacheOptions:Url"];
                var redisCache = new Redis_Cache<User>(dbNo, entityKey, url);
                var repository = sp.GetService<IRepositoryLetterApp<User>>();

                return new UserWithRedisService(redisCache, repository);
            });
            services.AddScoped<IDiaryWithRedisService, DiaryWithRedisService>(sp =>
            {
                var dbNo = RedisDatabase.Diaries;
                var entityKey = RedisEntityKey.DiaryKey;
                var url = configuration["CacheOptions:Url"];
                var redisCache = new Redis_Cache<Diary>(dbNo, entityKey, url);
                var repository = sp.GetService<IRepositoryLetterApp<Diary>>();

                return new DiaryWithRedisService(redisCache, repository);
            });

            services.AddScoped<IDiaryNoteWithRedisService, DiaryNoteWithRedisService>(sp =>
            {

                var dbNo = RedisDatabase.DiaryNotes;
                var entityKey = RedisEntityKey.DiaryNoteKey;
                var url = configuration["CacheOptions:Url"];
                var redisCache = new Redis_Cache<DiaryNote>(dbNo, entityKey, url);
                var repository = sp.GetService<IRepositoryLetterApp<DiaryNote>>();
                return new DiaryNoteWithRedisService(redisCache, repository);

            });
            //FluentValidation
           services.AddTransient(typeof(IValidationService<>), typeof(ValidationService<>));
            


        }

    }
}
