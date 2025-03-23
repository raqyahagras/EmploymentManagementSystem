using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmploymentManagementSystem.Core.Interfaces.ISpecifications;

namespace EmploymentManagementSystem.Infrastructure.Repositories
{
    public class SpecificationElevator<T> where T : class
    {

        public static IQueryable<T> Query_Factory(IQueryable<T> query, ISpecifications<T> specs)
        {
            if (specs.Criteria != null)
                query = query.Where(specs.Criteria);
            if (specs.Includes != null)
                foreach (var inc in specs.Includes)
                    query = query.Include(inc);

            return query.AsNoTracking();
        }
    }
}
