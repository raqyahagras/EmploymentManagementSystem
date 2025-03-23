using EmploymentManagementSystem.Core.DTOs;
using EmploymentManagementSystem.Core.Errors;
using EmploymentManagementSystem.Application;
using Hemdan.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentManagementSystem.Application
{
    public interface IPermissionService
    {


        Task<ApiResponse<PermissionToReturnDto>> AddPermission( PermissionDto model);
        Task<ApiResponse<bool>> AssignPermissionsToRoleAsync(string roleId, List<int> permissionIds);
        Task<ApiResponse<List<PermissionToReturnDto>>> GetAllPermissionsAsync();
        Task<ApiResponse<bool>> RoleHasPermissionAsync(string roleId, string permissionName);
        Task<ApiResponse<List<string>>> GetUserPermissionsAsync(string userId);
        Task<ApiResponse<List<PermissionToReturnDto>>> GetPermissionsForRoleAsync(string roleId);
        Task<ApiResponse<bool>> RemovePermissionFromRoleAsync(string roleId, int permissionId);
    }
}
