using EmploymentManagementSystem.Core.DTOs;
using EmploymentManagementSystem.Core.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentManagementSystem.Application
{
    public interface IRoleService
    {
        Task<ApiResponse<IEnumerable<RoleToReturnDto>>> GetAllRolesAsync();
        Task<ApiResponse<string>> AssignRoleToEmployeeAsync(string employeeId, string role);
        Task<ApiResponse<string>> RemoveRoleFromEmployee(string employeeId, string roleName);
    }

}
