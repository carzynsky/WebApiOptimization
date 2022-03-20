using System.Collections.Generic;
using System.Linq;
using WebApiOptimization.Core.Repositories.Base;
using WebApiOptimization.Infrastructure.Data;

namespace WebApiOptimization.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly NorthwndContext _northwndContext;

        public Repository(NorthwndContext northwndContext)
        {
            _northwndContext = northwndContext;
        }

        public T Add(T entity)
        {
            _northwndContext.Add(entity);
            _northwndContext.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            _northwndContext.Set<T>().Remove(entity);
            _northwndContext.SaveChanges();
        }

        public IReadOnlyList<T> GetAll()
        {
            IReadOnlyList<T> list = _northwndContext.Set<T>().ToList();
            return list;
        }

        public T GetById(int id)
        {
            return _northwndContext.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            _northwndContext.Set<T>().Update(entity);
            _northwndContext.SaveChanges();
        }
    }
}
