using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.EntityModel;

namespace SharpDev.Core
{
    public interface IRepository<T> where T : class
    {
        T Add(T Entity);

        IEnumerable<T> AddRange(IEnumerable<T> entities);

        T Update(T Entity);

        IEnumerable<T> Update(IEnumerable<T> entities);

        void Delete(T Entity);

        void Delete(object KeyValue);

        void Delete(Expression<Func<T, bool>> Expression);

        T FindById(object KeyValue);

        T FirstOrDefault(Expression<Func<T, bool>> Expression);

        T LastOrDefault(Expression<Func<T, bool>> Expression);

        IQueryable<T> All();

        IEnumerable<T> Where(Expression<Func<T, bool>> Expression);

        IEnumerable<T> Where(Expression<Func<T, bool>> Predicate, Expression<Func<T, string>> Order);

        PagedList<T> Paginate(IQueryable<T> query, int pageIndex = 0, int pageSize = int.MaxValue);

        int SaveChanges();

        IEnumerable<T> Select(System.Linq.Expressions.Expression<Func<T, int, T>> selector);

        IEnumerable<T> SelectMany(System.Linq.Expressions.Expression<Func<T, IEnumerable<T>>> selector);

        int Count();

        int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate);

        int Count(Func<T, bool> predicate);

        long LongCount();

        long LongCount(System.Linq.Expressions.Expression<Func<T, bool>> predicate);

        long LongCount(Func<T, bool> predicate);
    }
}
