using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        public T GetById<TKey>(TKey id);
        public IQueryable<T> GetAll();
        public void Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public IQueryable<T> Get(Expression<Func<T,bool>> expression);

    }
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = this._context.Set<T>();
        }
        public void Create(T entity)
        {
            _dbSet.Add(entity);
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return this._dbSet;
        }

        public T GetById<TKey>(TKey id)
        {
            return (T)_dbSet.Find(new object[1]{id});
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
