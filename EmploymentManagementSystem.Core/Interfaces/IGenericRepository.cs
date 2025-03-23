using System.Collections.Generic;
using static EmploymentManagementSystem.Core.Interfaces.ISpecifications;

namespace EmploymentManagementSystem.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        IEnumerable<T> GetAllWithSpec(ISpecifications<T> spec);
        T GetByIdWithSpec(ISpecifications<T> spec);
        int GetCountBySpec(ISpecifications<T> spec);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
