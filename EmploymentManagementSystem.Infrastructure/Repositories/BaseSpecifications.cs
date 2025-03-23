using EmploymentManagementSystem.API.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static EmploymentManagementSystem.Core.Interfaces.ISpecifications;

namespace EmploymentManagementSystem.Infrastructure.Repositories
{
    
        public class BaseSpecification<T> : ISpecifications<T> where T : class
        {
            public Expression<Func<T, bool>> Criteria { get; private set; } = null;

            public List<Expression<Func<T, object>>> Includes { get; private set; } = new List<Expression<Func<T, object>>>();

            public BaseSpecification(Expression<Func<T, bool>> _Criteria)
            {
                this.Criteria = _Criteria;
            }
            public void Add_Include(Expression<Func<T, object>> expression)
                => this.Includes.Add(expression);

            public void AddCriteria(Expression<Func<T, bool>> newCriteria)
            {
                Criteria = Criteria == null ? newCriteria : ExpressionCombiner.And(Criteria, newCriteria);
            }
        }
    }


