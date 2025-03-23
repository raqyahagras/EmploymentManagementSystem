using EmploymentManagementSystem.Infrastructure.Data;
using EmploymentManagementSystem.Core.DTOs;
using EmploymentManagementSystem.Core.Errors;
using EmploymentManagementSystem.Application;
using Hemdan.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EmploymentManagementSystem.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace EmploymentManagementSystem.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Employee> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApiExceptionResponse _responseHandler;

        public PermissionService(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, ApiExceptionResponse responseHandler, UserManager<Employee> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _responseHandler = responseHandler;
        }


        public async Task<ApiResponse<PermissionToReturnDto>> AddPermission( PermissionDto model )
        {
            // Get the current user's ID as a string
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) )
            {
                return _responseHandler.Unauthorized<PermissionToReturnDto>("User ID is invalid or missing.");
            }
            var permission = new Permission { Name = model.Name 
                , Created =DateTime.UtcNow 
              
                , Updated = DateTime.UtcNow
                
                };
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();

            var perm = new PermissionToReturnDto
            {
                Id = permission.Id,
                Name = permission.Name
            };


            return _responseHandler.Success<PermissionToReturnDto>(perm ,"permission added successfuly" );

        }



        public async Task<ApiResponse<bool>> AssignPermissionsToRoleAsync(string roleId, List<int> permissionIds)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return _responseHandler.Success(false, "There is no role with this ID");

            // Fetch permissions from DB
            var permissions = await _context.Permissions
                .Where(p => permissionIds.Contains(p.Id))
                .ToListAsync();

            if (!permissions.Any())
                return _responseHandler.Success(false, "No permissions exist with the provided IDs");

            // Check existing role permissions to avoid duplicates
            var existingPermissions = await _context.RolePermissions
                .Where(rp => rp.RoleId == roleId && permissionIds.Contains(rp.PermissionId))
                .Select(rp => rp.PermissionId)
                .ToListAsync();

            var newPermissions = permissions
                .Where(p => !existingPermissions.Contains(p.Id))
                .Select(p => new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = p.Id
                })
                .ToList();

            if (!newPermissions.Any())
                return _responseHandler.Success(false, "There are no new permissions to add");

            await _context.RolePermissions.AddRangeAsync(newPermissions);
            await _context.SaveChangesAsync();

            return _responseHandler.Success(true, "Permissions assigned to role successfully");
        }


        public async Task<ApiResponse<List<PermissionToReturnDto>>> GetAllPermissionsAsync()
        {
            //var language = _httpContextAccessor.HttpContext?.Request.Headers["Accept-Language"].ToString() ?? "en";

            var permissions = await _context.Permissions.ToListAsync();

              var perms=  permissions.Select(x => new PermissionToReturnDto
                {
                    Id = x.Id,
                    Name = x.Name.ToString(),
                    //Name = language == "ar" ? p.NameAr : p.Name
              })
                .ToList();

            return _responseHandler.Success(perms , "permissions returned successfully");


        }



        public async Task<ApiResponse<bool>> RoleHasPermissionAsync(string roleId, string permissionName)
        {
           bool Issuccess=  await _context.RolePermissions
                .AnyAsync(rp => rp.RoleId.ToString() == roleId && rp.Permission.Name.ToString() == permissionName);

            return _responseHandler.Success(Issuccess);
        
        }


        public async Task<ApiResponse<List<string>>> GetUserPermissionsAsync(string userId)
        {
            var roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(userId.ToString()));

            var permissions = await _context.RolePermissions
                .Where(rp => roles.Contains(rp.Role.Name))
                .Select(rp => rp.Permission.Name.ToString())
                .Distinct()
                .ToListAsync();

            return _responseHandler.Success(permissions , "user permissions returned successfuly");
        }

        public async Task<ApiResponse<List<PermissionToReturnDto>>> GetPermissionsForRoleAsync(string roleId)
        {
            var permissions = await _context.RolePermissions
                .Where(rp => rp.RoleId.ToString() == roleId)
                .Select(rp => new PermissionToReturnDto
                {
                    Id = rp.Permission.Id,
                    Name = rp.Permission.Name
                })
                .ToListAsync();


            return _responseHandler.Success(permissions, "Roles permissions returned successfuly");


        }




        public async Task<ApiResponse<bool>> RemovePermissionFromRoleAsync(string roleId, int permissionId)
        {
            bool exists = await _context.RolePermissions.AnyAsync(rp => rp.RoleId == roleId && rp.Permission.Id == permissionId);
            if (!exists)
                return _responseHandler.Success(true, "permissions not existed ");

            var rolePermission = await _context.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.Permission.Id == permissionId);

            _context.RolePermissions.Remove(rolePermission);
            await _context.SaveChangesAsync();
            return _responseHandler.Success(true, "permissions removed successfully");
        }




    }
}
