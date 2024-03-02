using LetterApp.BLL.AbsractServices;
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
    public class DiaryService : RepositoryLetterApp<Diary>, IDiaryService
    {
        private readonly LetterAppContext _context;

        public DiaryService(LetterAppContext context) : base(context)
        {
          _context = context;
        }

        public async Task<IEnumerable<object>> GetDiaryWithNote(int id)
        {
            var query =await  (from dn in _context.DiaryNotes
                         join d in _context.Diaries on dn.DiaryId equals d.Id
                         where dn.DiaryId == id
                         select new 
                         {
                             DiaryId = d.Id,
                             DiaryNoteId = dn.Id,
                             DiaryHeader = d.Header,
                             Title = dn.Title,
                             Body = dn.Body,
                             CreatedDate = dn.Created_Date

                         }).ToListAsync();

            return query;

        }
    }
}
