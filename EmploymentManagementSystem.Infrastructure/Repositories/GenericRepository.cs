using EmploymentManagementSystem.Infrastructure.Data;
using EmploymentManagementSystem.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>().ToList();
        }

        public T GetById(string id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAllWithSpec(ISpecifications<T> spec)
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
            dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
            dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            dbContext.SaveChanges();
        }
    }
}
