using AutoMapper;
using LetterApp.Api.DTOs;
using LetterApp.Api.DTOs.Diary;
using LetterApp.BLL.AbsractServices;
using LetterApp.BLL.AbstractWithRedisServices;
using LetterApp.BLL.FluentValidationServices;
using LetterApp.BLL.FluentValidationServices.ModelStateHelper;
using LetterApp.Dal.ProjectContext;
using LetterApp.Entity.Entities;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;
using System.Text.Json.Serialization;


namespace LetterApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryController : ControllerBase
    {
        private readonly IDiaryService _diaryService;
        private readonly IDiaryWithRedisService _diaryWithRedis;
        private readonly IMapper _mapper;
        private readonly IValidationService<DiaryDTO> _diaryValidation;
        private readonly IValidationService<DiaryEditDTO> _diaryEditValidation;
        private readonly LetterAppContext _context;

        public DiaryController(IDiaryService diaryService, IDiaryWithRedisService diaryWithRedis, IMapper mapper, IValidationService<DiaryDTO> diaryValidation, IValidationService<DiaryEditDTO> diaryEditValidation,LetterAppContext context)
        {
            _diaryService = diaryService;
            _diaryWithRedis = diaryWithRedis;
            _mapper = mapper;
            _diaryValidation = diaryValidation;
            _diaryEditValidation = diaryEditValidation;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

      

            var diaries = await _diaryWithRedis.GetAll();
            return diaries != null ? Ok(diaries) : BadRequest(false);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
         

         var test = await _diaryService.GetDiaryWithNote(id);
      
          return test != null ? Ok(test) : BadRequest(false);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(DiaryDTO diaryDTO)
        {
            ModelState.Clear();
            var errors = _diaryValidation.GetValidationErrors(diaryDTO);
            if (errors.Any())
            {
                return BadRequest(ValidationHelper.HandleValidationErrors(errors));
            }
            var newDiary = _mapper.Map<Diary>(diaryDTO);
            await _diaryWithRedis.Create(newDiary);
            return Ok(newDiary);

        }
        [HttpPut("edit")]
        public async Task<IActionResult> Edit(DiaryEditDTO editDTO)
        {
            ModelState.Clear();
            var errors = _diaryEditValidation.GetValidationErrors(editDTO);
            if (errors.Any())
            {
                return BadRequest(ValidationHelper.HandleValidationErrors(errors));
            }
            var updateDiary = _mapper.Map<Diary>(editDTO);
            var result = await _diaryWithRedis.Update(updateDiary);
            if(result==null)
            {
                return BadRequest(false);
            }
            return Ok(updateDiary);

        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _diaryWithRedis.Delete(id);
            return result ? Ok(result) : BadRequest(false);

        }
    }
}
