using SharpDev.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRepository.PostgreSql
{
    public partial class NpgSqlRepository<T> : IRepository<T> where T : class
    {
        public NpgSqlRepository()
        {
            
        }

        public T Add(T Entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public T Update(T Entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Update(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(T Entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object KeyValue)
        {
            throw new NotImplementedException();
        }

        public void Delete(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            throw new NotImplementedException();
        }

        public T FindById(object KeyValue)
        {
            throw new NotImplementedException();
        }

        public T FirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            throw new NotImplementedException();
        }

        public T LastOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> Predicate, System.Linq.Expressions.Expression<Func<T, string>> Order)
        {
            throw new NotImplementedException();
        }

        public PagedList<T> Paginate(IQueryable<T> query, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Select(System.Linq.Expressions.Expression<Func<T, int, T>> selector)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> SelectMany(System.Linq.Expressions.Expression<Func<T, IEnumerable<T>>> selector)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public int Count(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public long LongCount()
        {
            throw new NotImplementedException();
        }

        public long LongCount(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public long LongCount(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
