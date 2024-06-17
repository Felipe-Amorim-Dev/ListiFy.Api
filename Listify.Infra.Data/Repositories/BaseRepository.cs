using Listify.Domain.Interfaces.Repositories;
using Listify.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Infra.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        public virtual async Task CreateAsync(T entity)
        {
            using (var context = new DataContext())
            {
                context.Add(entity);
                context.SaveChanges();
            }
        }

        public virtual async Task UpdateAsync(T entity) 
        {
            using (var context = new DataContext())
            {
                context.Update(entity);
                context.SaveChanges();
            }
        }

        public virtual async Task DeleteAsync(T entity)
        {
            using (var context = new DataContext())
            {
                context.Remove(entity);
                context.SaveChanges();
            }
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            using (var context = new DataContext())
            {
                return context.Set<T>().ToList();
            }
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            using (var context = new DataContext())
            {
                return context.Set<T>().Find(id);
            }
        }
    }
}
