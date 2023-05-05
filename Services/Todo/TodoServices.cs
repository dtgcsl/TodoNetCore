using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoWebApi.Data;
using TodoWebApi.Dtos.Todo;


namespace TodoWebApi.Services.Todo
{
    public class TodoServices : ITodoServices
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public TodoServices(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        
        public async Task<ServiceResponse<List<GetTodoDto>>> AddTodo(AddTodoDto newTodo)
        {
            var serviceResponse = new ServiceResponse<List<GetTodoDto>>();
            var todo = _mapper.Map<Models.Todo>(newTodo);
            _context.Todo.Add(todo);
            await _context.SaveChangesAsync(); 
            serviceResponse.Data = _context.Todo.Include(t=>t.UserHasTodos).Select(c => _mapper.Map<GetTodoDto>(c)).ToList();
            return serviceResponse;
        }
        
        public async Task<ServiceResponse<List<GetTodoDto>>> GetAllTodo()
        {
            return new ServiceResponse<List<GetTodoDto>> { Data = _context.Todo.Include(t=> t.UserHasTodos).Select(c => _mapper.Map<GetTodoDto>(c)).ToList() };
        }

        public async Task<ServiceResponse<GetTodoDto>> GetTodoById(int id)
        {
            var serviceResponse = new ServiceResponse<GetTodoDto>();
            var todo = _context.Todo.Where(u=> u.todoId == id).Include(t=> t.UserHasTodos).FirstOrDefault();
            serviceResponse.Data = _mapper.Map<GetTodoDto>(todo);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTodoDto>> UpdateTodo(int id, UpdateTodoDto updatedTodo)
        {
            ServiceResponse<GetTodoDto> serviceResponse = new ServiceResponse<GetTodoDto>();
            try
            {
                var todo = _context.Todo.Include(t=> t.UserHasTodos).FirstOrDefault(c => c.todoId == id);
                _mapper.Map(updatedTodo, todo);
                serviceResponse.Data = _mapper.Map<GetTodoDto>(todo);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<List<GetTodoDto>>> DeleteTodo(int id)
        {
            ServiceResponse<List<GetTodoDto>> serviceResponse = new ServiceResponse<List<GetTodoDto>>();
            try
            {
                var todo = _context.Todo.Include(t=> t.UserHasTodos).FirstOrDefault(c => c.todoId == id);

                _context.Todo.Remove(todo);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.Todo.Select(c => _mapper.Map<GetTodoDto>(c)).ToList();
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }

        
        public async Task<ServiceResponse<GetAssignTodoDto>> AssignTodo(AddAssignTodoDto addAssignTodoDto)
        {
            ServiceResponse<GetAssignTodoDto> serviceResponse = new ServiceResponse<GetAssignTodoDto>();
            try
            {
                var todo = await  _context.Todo.FirstOrDefaultAsync(c => c.todoId == addAssignTodoDto.todoId);
                if (todo == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Todo not found";
                    return serviceResponse;
                }
                var user = await _context.User.FirstOrDefaultAsync(c => c.uid == addAssignTodoDto.uid);
                if (user == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User not found";
                    return serviceResponse;
                }

                var todoHasBeenAssign = await _context.UserHasTodos.FirstOrDefaultAsync(c =>
                    c.todoId == addAssignTodoDto.todoId && c.uid == addAssignTodoDto.uid);
                // Console.WriteLine(todoHasBeenAssign);
                if (todoHasBeenAssign != null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Todo has been assign before";
                    return serviceResponse;
                }
                
                UserHasTodos userHasTodos = _mapper.Map<UserHasTodos>(addAssignTodoDto);
                _context.UserHasTodos.Add(userHasTodos);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetAssignTodoDto>(addAssignTodoDto);
                serviceResponse.Message = "Todo has been assign to  User success";
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }
            

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetAssignTodoDto>>> UpdateAssignTodo(UpdateAssignTodoDto updateAssignTodoDto)
        {
            ServiceResponse<List<GetAssignTodoDto>> serviceResponse = new ServiceResponse<List<GetAssignTodoDto>>();
            try
            {
                var todo = await  _context.Todo.FirstOrDefaultAsync(c => c.todoId == updateAssignTodoDto.todoId);
                
                if (todo == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Todo not found";

                    return serviceResponse;
                }

                List<int> arrUid = updateAssignTodoDto.uid;
                
                List<UserHasTodos> user = _context.UserHasTodos.Where(u => arrUid.Contains(u.uid)).ToList();
                if (user.Count != arrUid.Count )
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Some of user not correct";
                    return serviceResponse;
                }
                
                _context.UserHasTodos.RemoveRange(user);
                
                arrUid.ForEach(uid =>
                {
                    AddAssignTodoDto addAssignTodoDto = new AddAssignTodoDto()
                        { todoId = updateAssignTodoDto.todoId, uid = uid };
                    UserHasTodos userHasTodos = _mapper.Map<UserHasTodos>(addAssignTodoDto);
                    _context.UserHasTodos.Add(userHasTodos);
                });
                serviceResponse.Data =
                    _context.UserHasTodos.Where(u => arrUid.Contains(u.uid)).Select(u => _mapper.Map<GetAssignTodoDto>(u)).ToList();
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetAssignTodoDto>>> DeleteAssignTodo(DeleteAssignTodoDto deleteAssignTodoDto)
        {
            ServiceResponse<List<GetAssignTodoDto>> serviceResponse = new ServiceResponse<List<GetAssignTodoDto>>();
            try
            {
                var todo = await  _context.Todo.FirstOrDefaultAsync(c => c.todoId == deleteAssignTodoDto.todoId);
                if (todo == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Todo not found";
                    return serviceResponse;
                }
                var user = await _context.User.FirstOrDefaultAsync(c => c.uid == deleteAssignTodoDto.uid);
                if (user == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User not found";
                    return serviceResponse;
                }

                var todoHasBeenAssign = await _context.UserHasTodos.FirstOrDefaultAsync(c =>
                    c.todoId == deleteAssignTodoDto.todoId && c.uid == deleteAssignTodoDto.uid);
                if (todoHasBeenAssign == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Todo never has been assign before";
                    return serviceResponse;
                }
                
                UserHasTodos assignTodos = _context.UserHasTodos.FirstOrDefault(u => u.uid==deleteAssignTodoDto.uid);

                _context.UserHasTodos.RemoveRange(assignTodos);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }
            serviceResponse.Data =
                _context.UserHasTodos.Select(u => _mapper.Map<GetAssignTodoDto>(u)).ToList();
            return serviceResponse;
        }
    }
}