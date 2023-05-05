using Microsoft.AspNetCore.Mvc;
using TodoWebApi.Dtos;
using TodoWebApi.Dtos.Todo;
using TodoWebApi.Services.Todo;

namespace TodoWebApi.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoServices _todoServices;

        public TodoController(ITodoServices todoServices)
        {
            _todoServices = todoServices;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetTodoDto>>>> AddTodo(AddTodoDto newTodo)
        {
            return Ok(await _todoServices.AddTodo(newTodo));
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetTodoDto>>>> GetAllTodo()
        {
            return Ok(await _todoServices.GetAllTodo());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetTodoDto>>> GetTodoById(int id)
        {
            return Ok(await _todoServices.GetTodoById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetTodoDto>>> UpdateTodo(int id, UpdateTodoDto updateTodoDto)
        {
            return Ok(await _todoServices.UpdateTodo(id, updateTodoDto));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetTodoDto>>>> DeleteTodo(int id)
        {
            return Ok(await _todoServices.DeleteTodo(id));
        }

        [HttpPost("/assignedTodo")]
        public async Task<ActionResult<ServiceResponse<GetTodoDto>>> AssignTodo(AddAssignTodoDto addAssignTodoDto)
        {
            return Ok(await _todoServices.AssignTodo(addAssignTodoDto));
        }
        
        [HttpPut("/assignedTodo")]
        public async Task<ActionResult<ServiceResponse<List<GetTodoDto>>>> UpdateAssignTodo(UpdateAssignTodoDto updateAssignTodoDto)
        {
            return Ok(await _todoServices.UpdateAssignTodo(updateAssignTodoDto));
        }
        
        [HttpDelete("/assignedTodo")]
        public async Task<ActionResult<ServiceResponse<GetTodoDto>>> DeleteAssignTodo(DeleteAssignTodoDto deleteAssignTodoDto)
        {
            return Ok(await _todoServices.DeleteAssignTodo(deleteAssignTodoDto));
        }
    }
}