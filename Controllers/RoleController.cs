using Microsoft.AspNetCore.Mvc;
using TodoWebApi.Dtos.Role;
using TodoWebApi.Services.Role;

namespace TodoWebApi.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetRoleDto>>> AddRole(AddRoleDto addRoleDto)
        {
            return Ok(await _roleService.AssignRole(addRoleDto));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetRoleDto>>>> UpdateRole(UpdateRoleDto updateRoleDto)
        {
            return Ok(await _roleService.UpdateRole(updateRoleDto));
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<GetRoleDto>>>> DeleteRole(DeleteRoleDto deleteRoleDto)
        {
            return Ok(await _roleService.DeleteRole(deleteRoleDto));
        }
    }

}