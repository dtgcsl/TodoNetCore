using AutoMapper;
using TodoWebApi.Dtos.Permission;
using TodoWebApi.Dtos.Role;
using TodoWebApi.Dtos.Todo;
using TodoWebApi.Dtos.User;

namespace TodoWebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            
            CreateMap<AddTodoDto, Todo>();
            CreateMap<Todo, GetTodoDto>().ReverseMap();
            CreateMap<UpdateTodoDto, Todo>();
            CreateMap<GetTodoDto, UserViewDto>();
            CreateMap<UserHasTodos,UserViewDto>();
            
            
            CreateMap<AddAssignTodoDto, UserHasTodos>();
            CreateMap<AddAssignTodoDto, GetAssignTodoDto>();
            CreateMap<UserHasTodos, GetAssignTodoDto>();

            CreateMap<AddUserDto, User>();
            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<UpdateUserDto, User>();
            CreateMap<Role, RoleViewDto>();
            CreateMap<UserHasTodos, TodoViewDto>();

            CreateMap<AddRoleDto, GetRoleDto>();
            CreateMap<AddRoleDto, Role>();
            CreateMap<AddRoleDto, GetAssignTodoDto>();

            CreateMap<AddPermissionDto, Permission>().ReverseMap();
            CreateMap<Permission, GetPermissionDto>();
            CreateMap<UpdatePermissionDto, Permission>();
            CreateMap<GetPermissionDto, RoleIdViewDto>();
            CreateMap<RoleHasPermissions, RoleIdViewDto>();
            
            CreateMap<AddAssignPermissionDto,GetAssignPermissionDto>();
            CreateMap<AddAssignPermissionDto,RoleHasPermissions>();
            CreateMap<RoleHasPermissions, GetAssignPermissionDto>();
        }
    }
}