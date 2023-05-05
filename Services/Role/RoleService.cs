using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoWebApi.Data;
using TodoWebApi.Dtos.Role;

namespace TodoWebApi.Services.Role
{

    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public RoleService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetRoleDto>> AssignRole(AddRoleDto addRoleDto)
        {
            ServiceResponse<GetRoleDto> serviceResponse = new ServiceResponse<GetRoleDto>();
            try
            {
                Models.User user = await _context.User.FirstOrDefaultAsync(c => c.uid == addRoleDto.uid);

                if (user == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User not found";
                    return serviceResponse;
                }

                Models.Role roleHasBeenAssign =
                    await _context.Role.FirstOrDefaultAsync(r => r.uid == addRoleDto.uid && r.name == addRoleDto.role);

                if (roleHasBeenAssign != null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User had this role";
                    return serviceResponse;
                }

                Models.Role role = _mapper.Map<Models.Role>(addRoleDto);
                _context.Role.Add(role);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetRoleDto>(addRoleDto);
                serviceResponse.Message = "Role has been assign  to User success";
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRoleDto>>> UpdateRole(UpdateRoleDto updateRoleDto)
        {
            ServiceResponse<List<GetRoleDto>> serviceResponse = new ServiceResponse<List<GetRoleDto>>();
            try
            {
                var user = await _context.User.FirstOrDefaultAsync(u => u.uid == updateRoleDto.uid);
                if (user == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User not correct";

                    return serviceResponse;
                }

                List<RoleEnum> arrRole = updateRoleDto.role;

                List<Models.Role> roles = _context.Role.Where(r => arrRole.Contains(r.name)).ToList();

                if (roles.Count != arrRole.Count)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Some of role not correct";
                    return serviceResponse;
                }

                _context.Role.RemoveRange(roles);

                arrRole.ForEach(r =>
                {
                    AddRoleDto addRoleDto = new AddRoleDto()
                    {
                        uid = updateRoleDto.uid,
                        role = r
                    };
                    Models.Role role = _mapper.Map<Models.Role>(addRoleDto);
                    _context.Role.Add(role);
                });
                serviceResponse.Data = _context.Role.Where(r => arrRole.Contains(r.name))
                    .Select(r => _mapper.Map<GetRoleDto>(r)).ToList();
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRoleDto>>> DeleteRole(DeleteRoleDto deleteRoleDto)
        {
            ServiceResponse<List<GetRoleDto>> serviceResponse = new ServiceResponse<List<GetRoleDto>>();
            try
            {
                var user = await _context.User.FirstOrDefaultAsync(c => c.uid == deleteRoleDto.uid);
                if (user == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User not found";
                    return serviceResponse;
                }

                var roleHasBeenAssign =
                    await _context.Role.FirstOrDefaultAsync(r =>
                        r.uid == deleteRoleDto.uid && r.name == deleteRoleDto.role);
                if (roleHasBeenAssign == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Role never has been assign before";
                    return serviceResponse;
                }

                Models.Role role = await _context.Role.FirstOrDefaultAsync(r => r.uid == deleteRoleDto.uid);
                
                _context.Role.RemoveRange(role);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            serviceResponse.Data = _context.Role.Select(r => _mapper.Map<GetRoleDto>(r)).ToList();
            return serviceResponse;
        }
    }

}