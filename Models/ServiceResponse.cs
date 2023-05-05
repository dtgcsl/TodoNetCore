namespace TodoWebApi.Models
{
    public class ServiceResponse<T>
    {
        public T? Data { set; get; }
        public bool Success { get; set; } = true;
        public string Message { set; get; } = string.Empty;
    }
}