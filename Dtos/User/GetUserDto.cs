using System.Runtime.Serialization;

namespace TodoWebApi.Dtos.User
{
    public class GetUserDto
    {
        public int uid {set;get;}
        public string name { get; set; }
        public ICollection<TodoViewDto> UserHasTodos { get; set; }
        public ICollection<RoleViewDto> Role { set; get; }
    }
}