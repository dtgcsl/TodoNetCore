using System.ComponentModel.DataAnnotations;

namespace TodoWebApi.Models
{
    public class Todo
    {
        [Key]
        public int todoId { get; set; }
        public string name { set; get; }
        public DateTimeOffset createAt { set; get; }
        public DateTimeOffset? updateAt { set; get; }
        public string? createdById { get; set; }

        public ICollection<UserHasTodos> UserHasTodos { get; set; }
    }
}