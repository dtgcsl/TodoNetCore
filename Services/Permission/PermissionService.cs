using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoWebApi.Data;
using TodoWebApi.Dtos.Permission;

namespace TodoWebApi.Services.Permission;

public class PermissionService : IPermissionService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public PermissionService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<ServiceResponse<List<GetPermissionDto>>> AddPermission(AddPermissionDto newPermission)
    {
        var serviceResponse = new ServiceResponse<List<GetPermissionDto>>();
        var permission = _mapper.Map<Models.Permission>(newPermission);
        _context.Permission.Add(permission);
        await _context.SaveChangesAsync();
        serviceResponse.Data = _context.Permission.Select(c => _mapper.Map<GetPermissionDto>(c)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetPermissionDto>>> GetAllPermission()
    {
        return new ServiceResponse<List<GetPermissionDto>> { Data = _context.Permission.Select(c => _mapper.Map<GetPermissionDto>(c)).ToList() };
    }
    
    public async Task<ServiceResponse<GetPermissionDto>> GetPermissionById(int id)
    {
        var serviceResponse = new ServiceResponse<GetPermissionDto>();
        var permission = await _context.Permission.Include(p=> p.RoleHasPermissions).FirstOrDefaultAsync(p => p.id == id);
        serviceResponse.Data = _mapper.Map<GetPermissionDto>(permission);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetPermissionDto>> UpdatePermission(int id, UpdatePermissionDto updatePermission)
    {
        ServiceResponse<GetPermissionDto> serviceResponse = new ServiceResponse<GetPermissionDto>();
        try
        {
            var Permission = await _context.Permission.FirstOrDefaultAsync(c => c.id == id);
            _mapper.Map(updatePermission, Permission);
            serviceResponse.Data = _mapper.Map<GetPermissionDto>(Permission);

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetPermissionDto>>> DeletePermission(int id)
    {
        ServiceResponse<List<GetPermissionDto>> serviceResponse = new ServiceResponse<List<GetPermissionDto>>();
        try
        {
            var Permission = await _context.Permission.FirstOrDefaultAsync(c => c.id == id);

            _context.Permission.Remove(Permission);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _context.Permission.Select(c => _mapper.Map<GetPermissionDto>(c)).ToList();
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetAssignPermissionDto>> AssignPermission(AddAssignPermissionDto addAssignPermissionDto)
    {
        ServiceResponse<GetAssignPermissionDto> serviceResponse = new ServiceResponse<GetAssignPermissionDto>();
        try
        {
            var Permission = await  _context.Permission.FirstOrDefaultAsync(c => c.id == addAssignPermissionDto.permissionId);
            if (Permission == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Permission not found";
                return serviceResponse;
            }
            var Role = await _context.Role.FirstOrDefaultAsync(c => c.rid == addAssignPermissionDto.rid);
            if (Role == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Role not found";
                return serviceResponse;
            }

            var PermissionHasBeenAssign = await _context.RoleHasPermissions.FirstOrDefaultAsync(c =>
                c.permissionId == addAssignPermissionDto.permissionId && c.rid == addAssignPermissionDto.rid);
            if (PermissionHasBeenAssign != null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Permission has been assign before";
                return serviceResponse;
            }
                
            RoleHasPermissions roleHasPermissions = _mapper.Map<RoleHasPermissions>(addAssignPermissionDto);
            _context.RoleHasPermissions.Add(roleHasPermissions);
            serviceResponse.Data = _mapper.Map<GetAssignPermissionDto>(addAssignPermissionDto);
            serviceResponse.Message = "Permission has been assign to  User success";
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }
            

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetAssignPermissionDto>>> UpdateAssignPermission(UpdateAssignPermissionDto updateAssignPermissionDto)
    {
        
        ServiceResponse<List<GetAssignPermissionDto>> serviceResponse = new ServiceResponse<List<GetAssignPermissionDto>>();
        try
        {
            var role = await  _context.Role.FirstOrDefaultAsync(c => c.rid == updateAssignPermissionDto.rid);
                
            if (role == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Permission not found";

                return serviceResponse;
            }

            List<int> arrpermissionId = updateAssignPermissionDto.permissionId;
                
            List<RoleHasPermissions> roleHasPermission = _context.RoleHasPermissions.Where(r => arrpermissionId.Contains(r.permissionId)).ToList();
            if (roleHasPermission.Count != arrpermissionId.Count )
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Some of Permission not correct";
                return serviceResponse;
            }
                
            _context.RoleHasPermissions.RemoveRange(roleHasPermission);
                
            arrpermissionId.ForEach(permissionId =>
            {
                AddAssignPermissionDto addAssignPermissionDto = new AddAssignPermissionDto()
                    { permissionId = permissionId, rid = updateAssignPermissionDto.rid };
                RoleHasPermissions roleHasPermissions = _mapper.Map<RoleHasPermissions>(addAssignPermissionDto);
                _context.RoleHasPermissions.Add(roleHasPermissions);
            });
            serviceResponse.Data =
                _context.RoleHasPermissions.Where(r => arrpermissionId.Contains(r.permissionId)).Select(r => _mapper.Map<GetAssignPermissionDto>(r)).ToList();
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;

    }

    public async Task<ServiceResponse<List<GetAssignPermissionDto>>> DeleteAssignPermission(DeleteAssignPermissionDto deleteAssignPermissionDto)
    {
        ServiceResponse<List<GetAssignPermissionDto>> serviceResponse = new ServiceResponse<List<GetAssignPermissionDto>>();
        try
        {
            var permission = await  _context.Permission.FirstOrDefaultAsync(p => p.id == deleteAssignPermissionDto.permissionId);
            if (permission == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Permission not found";
                return serviceResponse;
            }
            var role = await _context.Role.FirstOrDefaultAsync(r => r.rid == deleteAssignPermissionDto.rid);
            if (role == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Role not found";
                return serviceResponse;
            }

            var PermissionHasBeenAssign = await _context.RoleHasPermissions.FirstOrDefaultAsync(r =>
                r.permissionId == deleteAssignPermissionDto.permissionId && r.rid == deleteAssignPermissionDto.rid);
            if (PermissionHasBeenAssign == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Permission never has been assign before";
                return serviceResponse;
            }
                
            RoleHasPermissions roleHasPermissions = await _context.RoleHasPermissions.FirstOrDefaultAsync(r => r.rid==deleteAssignPermissionDto.rid);

            _context.RoleHasPermissions.RemoveRange(roleHasPermissions);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }
        serviceResponse.Data =
            _context.RoleHasPermissions.Select( r=> _mapper.Map<GetAssignPermissionDto>(r)).ToList();
        return serviceResponse;

    }
}