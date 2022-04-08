using System.Collections.Generic;

namespace WebApiOptimization.Core.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        abstract T GetById(int id);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entitites);
    }
}
