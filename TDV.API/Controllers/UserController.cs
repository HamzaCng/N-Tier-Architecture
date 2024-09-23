using Microsoft.AspNetCore.Mvc;
using TDV.Application.Shared.Users;
using TDV.Application.Shared.Users.Dtos;
using TDV.Entity.Entities.Users;

namespace TDV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userSevice;

        public UserController(IUserService userSevice)
        {
            _userSevice = userSevice;
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userSevice.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var users = await _userSevice.GetByIdAsync(id);
            if (users == null)
                return NotFound();
            return Ok(users);
        }

        [HttpGet("GetFullName/{id}")]
        public async Task<IActionResult> GetFullName(int id)
        {
            var users = await _userSevice.GetUserFullName(id);

            if (users == null)
                return NotFound();

            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrEditUserDto users)
        {
            if (users == null) return BadRequest();

            var user = new User
            {
                CreateDate = DateTime.Now,
                Email = users.Email,
                Name = users.Name,
                SurName = users.SurName
            };

            await _userSevice.AddAsync(user);
            return Ok(user);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(CreateOrEditUserDto users)
        {
            if(users.Id == null) return BadRequest();

            var user = new User
            {
                Id = users.Id,
                CreateDate = DateTime.Now,
                Email = users.Email,
                Name = users.Name,
                SurName = users.SurName
            };

            await _userSevice.Update(user);
            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userSevice.RemoveAsync(id);
            return Ok();
        }
    }
}
