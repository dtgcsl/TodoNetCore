using Microsoft.AspNetCore.Mvc;
using TodoWebApi.Dtos.User;
using TodoWebApi.Services.User;

namespace TodoWebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> AddUser(AddUserDto newUser)
        {
            return Ok(await _userServices.AddUser(newUser));
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetAllUserDto()
        {
            return Ok(await _userServices.GetAllUser());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUserById(int id)
        {
            return Ok(await _userServices.GetUserById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateUser(int id, UpdateUserDto updateUser)
        {
            return Ok(await _userServices.UpdateUser(id, updateUser));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> DeleteUser(int id)
        {
            return Ok(await _userServices.DeleteUser(id));
        }
    }

}