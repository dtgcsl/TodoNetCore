using Microsoft.AspNetCore.Mvc;
using TodoWebApi.Dtos;
using TodoWebApi.Dtos.Permission;
using TodoWebApi.Dtos.Todo;
using TodoWebApi.Services.Permission;
using TodoWebApi.Services.Todo;

namespace TodoWebApi.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetPermissionDto>>>> AddPermission(AddPermissionDto newPermission)
        {
            return Ok(await _permissionService.AddPermission(newPermission));
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetPermissionDto>>>> GetAllPermission()
        {
            return Ok(await _permissionService.GetAllPermission());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetPermissionDto>>> GetPermissionById(int id)
        {
            return Ok(await _permissionService.GetPermissionById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetPermissionDto>>> UpdatePermission(int id, UpdatePermissionDto updatePermissionDto)
        {
            return Ok(await _permissionService.UpdatePermission(id, updatePermissionDto));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetPermissionDto>>>> DeletePermission(int id)
        {
            return Ok(await _permissionService.DeletePermission(id));
        }

        [HttpPost("/assignedPermission")]
        public async Task<ActionResult<ServiceResponse<GetPermissionDto>>> AssignPermission(AddAssignPermissionDto addAssignPermissionDto)
        {
            return Ok(await _permissionService.AssignPermission(addAssignPermissionDto));
        }
        
        [HttpPut("/assignedPermission")]
        public async Task<ActionResult<ServiceResponse<List<GetPermissionDto>>>> UpdateAssignPermission(UpdateAssignPermissionDto updateAssignPermissionDto)
        {
            return Ok(await _permissionService.UpdateAssignPermission(updateAssignPermissionDto));
        }
        
        [HttpDelete("/assignedPermission")]
        public async Task<ActionResult<ServiceResponse<GetPermissionDto>>> DeleteAssignPermission(DeleteAssignPermissionDto deleteAssignPermissionDto)
        {
            return Ok(await _permissionService.DeleteAssignPermission(deleteAssignPermissionDto));
        }
    }
}