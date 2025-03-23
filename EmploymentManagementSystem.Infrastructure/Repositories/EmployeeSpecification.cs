using EmploymentManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentManagementSystem.Infrastructure.Repositories
{
    public class EmployeeSpecification : BaseSpecification<Employee>
    {
        public EmployeeSpecification(string name, string jobTitle, decimal? minSalary, decimal? maxSalary) : base(e => true)
        {
            if (!string.IsNullOrEmpty(name))
                AddCriteria(e => e.UserName.Contains(name));

            if (!string.IsNullOrEmpty(jobTitle))
                AddCriteria(e => e.JobTitle.Contains(jobTitle));

            if (minSalary.HasValue)
                AddCriteria(e => e.Salary >= minSalary.Value);

            if (maxSalary.HasValue)
                AddCriteria(e => e.Salary <= maxSalary.Value);
        }
    } 
    }
