using System.Runtime.Serialization;

namespace TodoWebApi.Dtos.User
{
    public class UpdateUserDto
    {
        public string name { get; set; }
        public string password { set; get; }
    }
}