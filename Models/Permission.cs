using System.ComponentModel.DataAnnotations;

namespace TodoWebApi.Models;

public class Permission
{
    [Key]
    public int id { get; set; }
    public string name { get; set; }

    public ICollection<RoleHasPermissions> RoleHasPermissions { get; set; }
}