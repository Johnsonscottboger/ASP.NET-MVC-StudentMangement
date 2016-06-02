using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Repository
{
    public interface IRepository<T> where T:class
    {
        bool Add(T entity);

        int Count(Expression<Func<T, bool>> predicate);

        bool Update(T entity);

        bool Delete(T entity);

        bool Exist(Expression<Func<T, bool>> anyLambda);

        T FindById(object id);

        T Find(Expression<Func<T, bool>> whereLambda);

        IEnumerable<T> FindAll();

        IList<T> FindPage(int pageIndex, int pageSize);

        IList<T> FindPage(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda);
    }
}
