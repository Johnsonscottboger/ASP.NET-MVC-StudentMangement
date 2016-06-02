using StudentManagement.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace StudentManagement.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private Context _context;
        public Repository(Context context)
        {
            this._context = context;
        }

        public bool Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges() > 0;
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Count(predicate);
        }

        public bool Delete(T entity)
        {
            if(_context.Entry(entity).State==EntityState.Deleted)
            {
                _context.Set<T>().Remove(entity);
            }
            return _context.SaveChanges() > 0;
        }

        public bool Exist(Expression<Func<T, bool>> anyLambda)
        {
            return _context.Set<T>().Any(anyLambda);
        }

        public T Find(Expression<Func<T, bool>> whereLambda)
        {
            return _context.Set<T>().FirstOrDefault<T>(whereLambda);
        }

        public IEnumerable<T> FindAll()
        {
            IEnumerable<T> query = _context.Set<T>();
            return query;
        }

        public IEnumerable<T> FindAll(Expression<Func<T,bool>> whereLambda)
        {
            IEnumerable<T> query = _context.Set<T>().Where(whereLambda);
            return query;
        }

        public T FindById(object id)
        {
            return _context.Set<T>().Find(id);
        }

        public IList<T> FindPage(int pageIndex, int pageSize)
        {
            IList<T> list = _context.Set<T>().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList<T>();
            return list;
        }

        public IList<T> FindPage(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda)
        {
            IList<T> list = _context.Set<T>().Where(whereLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList<T>();
            return list;
        }

        public bool Update()
        {
            return _context.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {
            //_context.Set<T>().Attach(entity);
            //_context.Entry(entity).State = EntityState.Modified;
            
            return _context.SaveChanges() > 0;
        }
    }
}