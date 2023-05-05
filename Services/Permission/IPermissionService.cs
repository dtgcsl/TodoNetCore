using TodoWebApi.Dtos.Permission;

namespace TodoWebApi.Services.Permission;

public interface IPermissionService
{
    Task<ServiceResponse<List<GetPermissionDto>>> AddPermission(AddPermissionDto newPermission);
    Task<ServiceResponse<List<GetPermissionDto>>> GetAllPermission();
    Task<ServiceResponse<GetPermissionDto>> GetPermissionById(int id);
    Task<ServiceResponse<GetPermissionDto>> UpdatePermission(int id,UpdatePermissionDto updatePermission);
    Task<ServiceResponse<List<GetPermissionDto>>> DeletePermission(int id);
        
        
    Task<ServiceResponse<GetAssignPermissionDto>> AssignPermission(AddAssignPermissionDto addAssignPermissionDto);
    Task<ServiceResponse<List<GetAssignPermissionDto>>> UpdateAssignPermission(UpdateAssignPermissionDto updateAssignPermissionDto);

    Task<ServiceResponse<List<GetAssignPermissionDto>>> DeleteAssignPermission(
        DeleteAssignPermissionDto deleteAssignPermissionDto);
}