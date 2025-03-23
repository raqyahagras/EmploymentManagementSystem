using EmploymentManagementSystem.Core.DTOs;
using EmploymentManagementSystem.Core.Entities;
using EmploymentManagementSystem.Core.Errors;
using System.Collections.Generic;
using static EmploymentManagementSystem.Core.Interfaces.ISpecifications;

namespace EmploymentManagementSystem.Application
{
    public interface IEmployeeService
    {
        Task<ApiResponse<IEnumerable<EmployeeModelDTO>>>GetAllEmployees();
        Task<ApiResponse<EmployeeModelDTO>> GetEmployeeById(string id);
       Task<ApiResponse<IEnumerable<EmployeeModelDTO>>> GetFilteredEmployees(ISpecifications<Employee> spec);
        Task<ApiResponse<string>> AddEmployee(AddEmployeeModelDTO user);
        Task<ApiResponse<string>> UpdateEmployee(EditEmployeeModelDTO user, string id);

        Task<ApiResponse<string>> DeleteEmployee(string id);
    }
}
