using EmploymentManagementSystem.Core.DTOs;
using EmploymentManagementSystem.Core.Entities;
using EmploymentManagementSystem.Core.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentManagementSystem.Application
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Employee> _userManager;
        private readonly ApiExceptionResponse _responseHandler;

        public RoleService(
            RoleManager<IdentityRole> roleManager,
            UserManager<Employee> userManager,
            ApiExceptionResponse responseHandler)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _responseHandler = responseHandler;
        }

        public async Task<ApiResponse<IEnumerable<RoleToReturnDto>>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleDtos = roles.Select(role => new RoleToReturnDto
            {
                RoleID = role.Id,
                RoleName = role.Name
            }).ToList();

            return _responseHandler.Success<IEnumerable<RoleToReturnDto>>(roleDtos, "Roles retrieved successfully.");
        }
        public async Task<ApiResponse<string>> AssignRoleToEmployeeAsync(string employeeId, string role)
        {
            var user = await _userManager.FindByIdAsync(employeeId);
            if (user == null)
                return _responseHandler.NotFound<string>("Employee not found.");

            if (!await _roleManager.RoleExistsAsync(role))
                return _responseHandler.BadRequest<string>("Role does not exist.");

            var result = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded)
            {
                return _responseHandler.BadRequest<string>("Failed to assign role: " +
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return _responseHandler.Success("Role assigned successfully.");
        }


        public async Task<ApiResponse<string>> RemoveRoleFromEmployee(string employeeId, string roleName)
        {
            var employee = await _userManager.FindByIdAsync(employeeId);
            if (employee == null)
            {
                return _responseHandler.NotFound<string>("Employee not found.");
            }

            var hasRole = await _userManager.IsInRoleAsync(employee, roleName);
            if (!hasRole)
            {
                return _responseHandler.BadRequest<string>("Employee does not have this role.");
            }

            var result = await _userManager.RemoveFromRoleAsync(employee, roleName);
            if (!result.Succeeded)
            {
                return _responseHandler.BadRequest<string>("Failed to remove role: " +
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return _responseHandler.Success("Role removed successfully.");
        }
    }

}
