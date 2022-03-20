using System.Collections.Generic;

namespace WebApiOptimization.Core.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        IReadOnlyList<T> GetAll();
        T GetById(int id);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity); 
    }
}
