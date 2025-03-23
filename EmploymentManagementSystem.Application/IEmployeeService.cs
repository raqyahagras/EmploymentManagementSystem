using EmploymentManagementSystem.Core.Entities;
using System.Collections.Generic;
using static EmploymentManagementSystem.Core.Interfaces.ISpecifications;

namespace EmploymentManagementSystem.Application
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
        IEnumerable<Employee> GetFilteredEmployees(ISpecifications<Employee> spec);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        bool DeleteEmployee(int id);
    }
}
