namespace TodoWebApi.Dtos.Todo
{
    public class UpdateTodoDto
    {
        public string name { set; get; } = "Giang";
        public DateTimeOffset createAt { set; get; }
        public DateTimeOffset? updateAt { set; get; } = DateTimeOffset.Now;
        public string? createdById { get; set; } = null;
    }
}