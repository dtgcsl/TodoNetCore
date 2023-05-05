namespace TodoWebApi.Dtos.Todo
{

    public class UpdateAssignTodoDto
    {
        public int todoId { get; set; }
        public List<int> uid { set; get; }
    }

}