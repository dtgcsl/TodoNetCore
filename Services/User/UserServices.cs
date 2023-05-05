using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TodoWebApi.Data;
using TodoWebApi.Dtos.User;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace TodoWebApi.Services.User
{
    public class UserServices : IUserServices
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserServices(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetUserDto>> AddUser(AddUserDto newUser)
        {
            newUser.password = BCrypt.Net.BCrypt.HashPassword(newUser.password);
            var serviceResponse = new ServiceResponse<GetUserDto>();
            Models.User user = _mapper.Map<Models.User>(newUser);
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUser()
        {
            return new ServiceResponse<List<GetUserDto>>()
            {
                Data = _context.User.Include(u => u.UserHasTodos)
                    .Include(u => u.Role).Select(c => _mapper.Map<GetUserDto>(c)).ToList()
            };
        }

        public async Task<ServiceResponse<GetUserDto>> GetUserById(int id)
        {
            var user = _context.User.Where(u => u.uid == id).Include(u => u.UserHasTodos)
                .Include(u => u.Role).FirstOrDefault();
            var serviceResponse = new ServiceResponse<GetUserDto>();
            serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(int id, UpdateUserDto updateUser)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();
            try
            {
                updateUser.password = BCrypt.Net.BCrypt.HashPassword(updateUser.password);
                Models.User user = _context.User.Include(u => u.UserHasTodos)
                    .Include(u => u.Role).FirstOrDefault(c => c.uid == id);
                _mapper.Map(updateUser, user);
                response.Data = _mapper.Map<GetUserDto>(user);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        [SuppressMessage("ReSharper.DPA", "DPA0007: Large number of DB records", MessageId = "count: 1008")]
        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id)
        {
            ServiceResponse<List<GetUserDto>> response = new ServiceResponse<List<GetUserDto>>();
            try
            {
                Models.User user = _context.User.Include(u => u.UserHasTodos)
                    .Include(u => u.Role).FirstOrDefault(c => c.uid == id);

                if (user == null)
                {
                    throw new Exception("User not found");
                }
                
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
                response.Data = _context.User.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }
    }
}