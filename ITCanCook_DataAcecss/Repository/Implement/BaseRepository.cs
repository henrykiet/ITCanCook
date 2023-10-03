using ITCanCook_DataAcecss.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Repository.Implement
{

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
		private readonly DbContext _context;
		private readonly DbSet<T> _dbSet;
		public BaseRepository(DbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}
		public void Create(T entity)
		{
			_dbSet.Add(entity);
			_context.SaveChanges();
		}
		public void Delete(T entity)
		{
			_dbSet.Remove(entity);
			_context.SaveChanges();
		}

		public IQueryable<T> GetAll()
		{
			return _dbSet;
		}

		public T GetById<TKey>(TKey id)
		{
			return _dbSet.Find(new object[1] { id });
		}

		public void Update(T entity)
		{
			_dbSet.Update(entity);
			_context.SaveChanges();
		}

		public IQueryable<T> Get(Expression<Func<T, bool>> expression)
		{
			return _dbSet.Where(expression);
		}

		public void DetachEntity(T entity)
		{
			_context.Entry(entity).State = EntityState.Detached;
		}
	}
}
