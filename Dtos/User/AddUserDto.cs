using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TodoWebApi.Dtos.User;

public class AddUserDto
{
    public string name { get; set; }
    public string password { set; get; }
}