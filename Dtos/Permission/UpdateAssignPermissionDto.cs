namespace TodoWebApi.Dtos.Permission;

public class UpdateAssignPermissionDto
{
    public int rid { get; set; }
    public List<int> permissionId { get; set; }
}