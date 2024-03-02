using LetterApp.BLL.AbsractService;
using LetterApp.BLL.Repository;
using LetterApp.Dal.ProjectContext;
using LetterApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterApp.BLL.Services
{
    public class UserService : RepositoryLetterApp<User>, IUserService 
    {
        public UserService(LetterAppContext context) : base(context)
        {
        }
    }
}
