namespace TodoWebApi.Dtos.Todo
{
    public class GetTodoDto
    {
        public int todoId { get; set; }
        public string name { set; get; } = "Giang";
        public DateTimeOffset createAt { set; get; } = DateTimeOffset.Now;
        public DateTimeOffset? updateAt { set; get; } = null;
        public string? createdById { get; set; } = null;
        public ICollection<UserViewDto> UserHasTodos { get; set; }
    }
}