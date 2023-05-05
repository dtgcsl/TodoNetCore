
using System.ComponentModel.DataAnnotations;

namespace TodoWebApi.Dtos.Todo
{

    public class AddAssignTodoDto
    {
        [Required]
        public int uid { get; set; }
        public int todoId { set; get; }
    }

}