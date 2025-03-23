using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentManagementSystem.Core.Interfaces
{
    public interface ISpecifications
    {
        public interface ISpecifications<T> where T : class
        {
            Expression<Func<T, bool>> Criteria { get; }
            List<Expression<Func<T, object>>> Includes { get; }
        }
    }
}
