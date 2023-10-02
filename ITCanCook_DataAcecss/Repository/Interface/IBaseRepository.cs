using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Repository.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        public T GetById<TKey>(TKey id);
        public IQueryable<T> GetAll();
        public void Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public IQueryable<T> Get(Expression<Func<T, bool>> expression);

    }
}
