using LetterApp.BLL.AbsractService;
using LetterApp.BLL.Repository;
using LetterApp.Dal.ProjectContext;
using LetterApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterApp.BLL.Services
{
    public class UserService : RepositoryLetterApp<User>, IUserService 
    {
        private readonly LetterAppContext _context;

        public UserService(LetterAppContext context) : base(context)
        {
           _context = context;
        }

        public async Task<User> GetByIdentity(string identityId)
        {
          var user = await _context.Set<User>().Where(x=>x.IdentityId == identityId).FirstOrDefaultAsync();
            return user;
        }
    }
}
