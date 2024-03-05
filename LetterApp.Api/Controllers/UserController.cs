using AutoMapper;
using LetterApp.Api.DTOs;
using LetterApp.Api.DTOs.User;
using LetterApp.Api.Validators;
using LetterApp.BLL.AbsractService;
using LetterApp.BLL.AbstractWithRedisServices;
using LetterApp.BLL.FluentValidationServices;
using LetterApp.BLL.FluentValidationServices.ModelStateHelper;
using LetterApp.Entity.BaseEntity;
using LetterApp.Entity.Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace LetterApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;
        private readonly IUserWithRedisService _userWithRedis;
        private readonly IValidationService<UserDTO> _validation;
        private readonly IMapper _mapper;

        public UserController(IUserService user, IUserWithRedisService userWithRedis,IValidationService<UserDTO> validation,IMapper mapper)
        {
            _user = user;
            _userWithRedis = userWithRedis;
            _validation = validation;
            _mapper = mapper;
         
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _user.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
          
            var user = await _userWithRedis.GetById(id);
            if (user == null) return NotFound("Id bulunamadı");
            return Ok(user);
         
        }
        [HttpGet("identity/{id}")]
        public async Task<IActionResult> GetUserByIdentity(string id)
        {

            var user = await _user.GetByIdentity(id);
            if (user == null) return NotFound("Id bulunamadı");
            return Ok(user);

        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDTO createDTO)
        {
            var newUser = _mapper.Map<User>(createDTO);
            await _userWithRedis.Create(newUser);
            return Ok(newUser);
        }

        [HttpPut("update/{updatedUser}")]
        public async Task<IActionResult> UpdateUser(UserDTO userDto)
        {
            var errors = _validation.GetValidationErrors(userDto);
            if(errors.Any()) return BadRequest(ValidationHelper.HandleValidationErrors(errors));

            userDto.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(userDto.Password, 13);
            userDto.PasswordConfirmed = BCrypt.Net.BCrypt.EnhancedHashPassword(userDto.PasswordConfirmed, 13);
            var updatedUser=_mapper.Map<User>(userDto);
            await _userWithRedis.Update(updatedUser,updatedUser.Id);
            return Ok(updatedUser);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userWithRedis.Delete(id);
            return result ? Ok() : BadRequest();


        }
   
       
    }
}
