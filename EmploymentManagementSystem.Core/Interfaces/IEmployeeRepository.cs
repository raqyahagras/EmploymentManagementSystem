using EmploymentManagementSystem.Core.Entities;
using EmploymentManagementSystem.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentManagementSystem.Core.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IEnumerable<Employee> GetEmployeesWithFilter(string name, string jobTitle, decimal? minSalary, decimal? maxSalary);

    }
}
