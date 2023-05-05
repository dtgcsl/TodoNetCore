
using System.ComponentModel.DataAnnotations;

namespace TodoWebApi.Dtos.Role
{

    public class AddRoleDto
    {
        [Required]
        public int uid { get; set; }
        public RoleEnum role { get; set; }
    }

}