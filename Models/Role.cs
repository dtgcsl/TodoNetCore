using System.ComponentModel.DataAnnotations;

namespace TodoWebApi.Models
{

    public class Role
    {
        [Key]
        public int rid { get; set; }
        public RoleEnum name { get; set; }
        public int uid { get; set; }
        
        public User User { set; get; }
        
        public ICollection<RoleHasPermissions> RoleHasPermissions { set; get; }

    }

}