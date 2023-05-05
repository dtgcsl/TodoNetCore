
using TodoWebApi.Dtos.User;

namespace TodoWebApi.Services.User
{

    public interface IUserServices
    {
        
        Task<ServiceResponse<GetUserDto>> AddUser(AddUserDto newUser);
        Task<ServiceResponse<List<GetUserDto>>> GetAllUser();
        Task<ServiceResponse<GetUserDto>> GetUserById(int id);
        Task<ServiceResponse<GetUserDto>> UpdateUser(int id, UpdateUserDto updateUser);
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id);
    }
}