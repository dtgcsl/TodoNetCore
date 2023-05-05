using TodoWebApi.Dtos;
using TodoWebApi.Dtos.Todo;
using TodoWebApi.Dtos.User;

namespace TodoWebApi.Services.Todo
{
    public interface ITodoServices
    {
        Task<ServiceResponse<List<GetTodoDto>>> AddTodo(AddTodoDto newTodo);
        Task<ServiceResponse<List<GetTodoDto>>> GetAllTodo();
        Task<ServiceResponse<GetTodoDto>> GetTodoById(int id);
        Task<ServiceResponse<GetTodoDto>> UpdateTodo(int id,UpdateTodoDto updateTodo);
        Task<ServiceResponse<List<GetTodoDto>>> DeleteTodo(int id);
        
        
        Task<ServiceResponse<GetAssignTodoDto>> AssignTodo(AddAssignTodoDto addAssignTodoDto);
        Task<ServiceResponse<List<GetAssignTodoDto>>> UpdateAssignTodo(UpdateAssignTodoDto updateAssignTodoDto);
        Task<ServiceResponse<List<GetAssignTodoDto>>> DeleteAssignTodo(DeleteAssignTodoDto deleteAssignTodoDto);
    }
}