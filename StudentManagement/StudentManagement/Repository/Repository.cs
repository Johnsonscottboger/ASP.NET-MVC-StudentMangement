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

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges() > 0;
        }

        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Count(predicate);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            if(_context.Entry(entity).State==EntityState.Deleted)
            {
                _context.Set<T>().Remove(entity);
            }
            return _context.SaveChanges() > 0;
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="anyLambda"></param>
        /// <returns></returns>
        public bool Exist(Expression<Func<T, bool>> anyLambda)
        {
            return _context.Set<T>().Any(anyLambda);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public T Find(Expression<Func<T, bool>> whereLambda)
        {
            return _context.Set<T>().FirstOrDefault<T>(whereLambda);
        }

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> FindAll()
        {
            IEnumerable<T> query = _context.Set<T>();
            return query;
        }

        /// <summary>
        /// 根据条件查找所有
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IEnumerable<T> FindAll(Expression<Func<T,bool>> whereLambda)
        {
            IEnumerable<T> query = _context.Set<T>().Where(whereLambda);
            return query;
        }

        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
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