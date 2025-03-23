using EmploymentManagementSystem.Infrastructure.Data;
using EmploymentManagementSystem.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmploymentManagementSystem.Core.Interfaces.ISpecifications;

namespace EmploymentManagementSystem.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;

        public GenericRepository(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public IReadOnlyList<T> GetAll()
        {
            return dbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public IReadOnlyList<T> GetAllWithSpec(ISpecifications<T> spec)
        {
            return ApplySpecification(spec).ToList();
        }

        public T GetByIdWithSpec(ISpecifications<T> spec)
        {
            return ApplySpecification(spec).FirstOrDefault();
        }

        public int GetCountBySpec(ISpecifications<T> spec)
        {
            return ApplySpecification(spec).Count();
        }

        private IQueryable<T> ApplySpecification(ISpecifications<T> spec)
        {
            return SpecificationElevator<T>.Query_Factory(dbContext.Set<T>(), spec);
        }

        public void Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }
    }

}
