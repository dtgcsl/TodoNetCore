using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TodoWebApi.Models
{

    public class RoleHasPermissions
    {
        public int rid { get; set; }
        public int permissionId { get; set; }

        public Role Role { set; get; }
        public Permission Permission { set; get; }
    }

}