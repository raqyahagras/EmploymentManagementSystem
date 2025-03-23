using EmploymentManagementSystem.Core.Entities;
using EmploymentManagementSystem.Core.Interfaces;
using EmploymentManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentManagementSystem.Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesWithFilter(string name, string jobTitle, decimal? minSalary, decimal? maxSalary)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(e => e.UserName.Contains(name));

            if (!string.IsNullOrEmpty(jobTitle))
                query = query.Where(e => e.JobTitle.Contains(jobTitle));

            if (minSalary.HasValue)
                query = query.Where(e => e.Salary >= minSalary.Value);

            if (maxSalary.HasValue)
                query = query.Where(e => e.Salary <= maxSalary.Value);

            return query.ToList();
        }
    }
}
