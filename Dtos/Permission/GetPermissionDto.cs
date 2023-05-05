using TodoWebApi.Dtos.Role;
using TodoWebApi.Dtos.User;

namespace TodoWebApi.Dtos.Permission;

public class GetPermissionDto
{
    public int id { get; set; }
    public string name { get; set; }
    public ICollection<RoleIdViewDto> RoleHasPermissions { get; set; }
}