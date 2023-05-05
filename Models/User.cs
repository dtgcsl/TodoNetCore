using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TodoWebApi.Models
{
    public class User
    {
        [Key] public int uid { set; get; }
        public string name { get; set; } 
        public string password { set; get; }


        public ICollection<UserHasTodos> UserHasTodos { get; set; }
        public ICollection<Role> Role { set; get; }
    }
}