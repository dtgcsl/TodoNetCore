namespace TodoWebApi.Dtos.Todo
{
    public class AddTodoDto
    {
        public string name { set; get; }
        public DateTimeOffset createAt { set; get; } = DateTimeOffset.Now;
        public DateTimeOffset? updateAt { set; get; } = null;
        public string? createdById { get; set; } = null;
    }
}