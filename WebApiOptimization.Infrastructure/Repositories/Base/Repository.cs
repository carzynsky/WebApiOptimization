using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiOptimization.Core.Repositories.Base;
using WebApiOptimization.Infrastructure.Data;

namespace WebApiOptimization.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly NorthwndContext NorthwndContext;

        public Repository(NorthwndContext northwndContext)
        {
            NorthwndContext = northwndContext;
        }

        public T Add(T entity)
        {
            NorthwndContext.Add(entity);
            NorthwndContext.SaveChanges();
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            await NorthwndContext.AddAsync(entity);
            await NorthwndContext.SaveChangesAsync();
            return entity;
        }

        public void Delete(T entity)
        {
            NorthwndContext.Set<T>().Remove(entity);
            NorthwndContext.SaveChanges();
        }

        public void DeleteRange(IEnumerable<T> entitites)
        {
            NorthwndContext.Set<T>().RemoveRange(entitites);
            NorthwndContext.SaveChanges();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return NorthwndContext.Set<T>().AsNoTracking();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await NorthwndContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual T GetById(int id)
        {
            return NorthwndContext.Set<T>().Find(id);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await NorthwndContext.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            NorthwndContext.Set<T>().Update(entity);
            NorthwndContext.SaveChanges();
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            NorthwndContext.Set<T>().UpdateRange(entities);
            NorthwndContext.SaveChanges();
        }
    }
}
