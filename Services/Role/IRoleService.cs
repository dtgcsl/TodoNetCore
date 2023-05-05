using TodoWebApi.Dtos.Role;

namespace TodoWebApi.Services.Role
{

    public interface IRoleService
    {
        Task<ServiceResponse<GetRoleDto>> AssignRole(AddRoleDto addRoleDto);
        Task<ServiceResponse<List<GetRoleDto>>> UpdateRole(UpdateRoleDto updateRoleDto);
        Task<ServiceResponse<List<GetRoleDto>>> DeleteRole(DeleteRoleDto deleteRoleDto);

    }

}