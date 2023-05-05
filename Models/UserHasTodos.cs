using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TodoWebApi.Models;

public class UserHasTodos
{
    public int todoId { set; get; }
    public int uid { set; get; }
    
    public User User { get; set; }
    public Todo Todo { get; set; }
}