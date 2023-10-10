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




		public T GetByIdB(string id)
		{
			try
			{
				return _dbSet.Find(id);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return null;
			}

		}

		public T GetEntity(T entity)
		{
			try
			{
				return _dbSet.Find(entity);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return null;
			}

		}
		public bool Insert(T entity)
		{
			try
			{
				_context.Set<T>().Add(entity);
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}

		}
		public bool UpdateB(T entity)
		{
			try
			{
				_context.Attach(entity);
				_context.Entry(entity).State = EntityState.Modified;
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}
		public bool DeleteB(T entity)
		{
			try
			{
				/*EntityEntry entityEntry = _context.Entry<T>(entity);
                entityEntry.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;*/
				_context.Set<T>().Remove(entity);
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return false;
			}
		}
	}
}
