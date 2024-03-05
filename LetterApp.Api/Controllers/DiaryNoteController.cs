using AutoMapper;
using FluentValidation;
using LetterApp.Api.DTOs.DiaryNote;
using LetterApp.BLL.AbstractWithRedisServices;
using LetterApp.BLL.FluentValidationServices;
using LetterApp.BLL.FluentValidationServices.ModelStateHelper;
using LetterApp.Dal.ProjectContext;
using LetterApp.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LetterApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryNoteController : ControllerBase
    {
        private readonly IDiaryNoteWithRedisService _diaryNoteWithRedis;
        private readonly IMapper _mapper;
        private readonly IValidationService<DiaryNoteCreateDTO> _validatorCreate;
        private readonly IValidationService<DiaryNoteEditDTO> _validatorEdit;
    

        public DiaryNoteController(IDiaryNoteWithRedisService diaryNoteWithRedis,IMapper mapper,IValidationService<DiaryNoteCreateDTO> validatorCreate, IValidationService<DiaryNoteEditDTO> validatorEdit)
        {
            _diaryNoteWithRedis = diaryNoteWithRedis;
            _mapper = mapper;
            _validatorCreate = validatorCreate;
            _validatorEdit = validatorEdit;
         
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            var diaryNotes = await _diaryNoteWithRedis.GetAll();
            var test = diaryNotes.Where(x => x.Id == 2).ToList();
       

            return Ok(test);
        //    return diaryNotes  != null ? Ok(diaryNotes): NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var dNote = await _diaryNoteWithRedis.GetById(id);
            return dNote != null ? Ok(dNote) : BadRequest(false);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(DiaryNoteCreateDTO diaryNoteDTO)
        {
            var errors = _validatorCreate.GetValidationErrors(diaryNoteDTO);
            if (errors.Any())
            {
                return BadRequest(ValidationHelper.HandleValidationErrors(errors));
            }
            var newDiaryNote = _mapper.Map<DiaryNote>(diaryNoteDTO);
            var result = await _diaryNoteWithRedis.Create(newDiaryNote);
            return Ok(result);

        }
        [HttpPut("edit")]
        public async Task<IActionResult> Edit(DiaryNoteEditDTO noteEditDTO)
        {
            var errors = _validatorEdit.GetValidationErrors(noteEditDTO);
            if (errors.Any())
            {
                return BadRequest(ValidationHelper.HandleValidationErrors(errors));
            }
            var updateDiaryNote = _mapper.Map<DiaryNote>(noteEditDTO);
            var result = await _diaryNoteWithRedis.Update(updateDiaryNote, updateDiaryNote.Id);
            return result !=null ? Ok(result) : BadRequest(true);

        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _diaryNoteWithRedis.Delete(id);
            return result ? Ok(true) : BadRequest(false);
        }
    }
}
