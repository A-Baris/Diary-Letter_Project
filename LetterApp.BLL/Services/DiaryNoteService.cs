using LetterApp.BLL.AbsractServices;
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
    public class DiaryNoteService : RepositoryLetterApp<DiaryNote>, IDiaryNoteService
    {
        public DiaryNoteService(LetterAppContext context) : base(context)
        {
        }
    }
}
