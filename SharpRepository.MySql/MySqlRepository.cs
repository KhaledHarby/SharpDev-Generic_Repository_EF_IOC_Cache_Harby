using SharpDev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.Common;
using MySql.Data.MySqlClient;
using MySql.Data.Entity;
using System.Data.Entity;
using System.Data.Common;
using System.Data;

namespace SharpRepository.MySql
{
    public partial class MySqlRepository<T> : IRepository<T>, IRepositoryExtension<T>, IRepositoryAsyncExtension<T>, IRepositoryAsync<T> where T : class
    {

        private MySqlHistoryContext _context;

        public MySqlRepository(string connectionString)
        {
            _context = new MySqlHistoryContext(new MySqlConnection(connectionString), string.Empty);           
        }

        public MySqlRepository(MySqlConnection connection)
        {
            _context = new MySqlHistoryContext(connection, string.Empty);
        }

        public MySqlRepository(MySqlHistoryContext Context)
        {
            _context = Context;
        }

        public T Add(T Entity)
        {
            _context.Set<T>().Add(Entity);
            _context.SaveChanges();
            return Entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
            return entities;
        }

        public T Update(T Entity)
        {
            _context.Entry<T>(Entity).State = EntityState.Modified;
            _context.SaveChanges();
            return Entity;
        }

        public IEnumerable<T> Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry<T>(entity).State = EntityState.Modified;
            }
            _context.SaveChanges();
            return entities;
        }

        public void Delete(T Entity)
        {
            _context.Entry<T>(Entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void Delete(object KeyValue)
        {
            _context.Entry<T>(_context.Set<T>().Find(KeyValue)).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void Delete(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            var Entities = _context.Set<T>().Where(Expression).ToList();
            foreach (var entity in Entities)
            {
                _context.Entry<T>(entity).State = EntityState.Deleted;
            }
            _context.SaveChanges();
        }

        public T FindById(object KeyValue)
        {
            return _context.Set<T>().Find(KeyValue);         
        }

        public T FirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            return _context.Set<T>().FirstOrDefault(Expression);
        }

        public T LastOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            return _context.Set<T>().LastOrDefault(Expression);
        }

        public IQueryable<T> All()
        {
            return _context.Set<T>().AsQueryable();
        }

        public IEnumerable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            return _context.Set<T>().Where(Expression).ToList();
        }

        public IEnumerable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> Predicate, System.Linq.Expressions.Expression<Func<T, string>> Order)
        {
            return _context.Set<T>().Where(Predicate).OrderBy(Order).ToList();
        }

        public PagedList<T> Paginate(IQueryable<T> query, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return new PagedList<T>(query, pageIndex, pageSize);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public IEnumerable<T> Select(System.Linq.Expressions.Expression<Func<T, int, T>> selector)
        {
            return _context.Set<T>().Select(selector).ToList();
        }

        public IEnumerable<T> SelectMany(System.Linq.Expressions.Expression<Func<T, IEnumerable<T>>> selector)
        {
            return _context.Set<T>().SelectMany(selector).ToList();
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Count(predicate);
        }

        public int Count(Func<T, bool> predicate)
        {
            return _context.Set<T>().Count(predicate);
        }

        public long LongCount()
        {
            return _context.Set<T>().LongCount();
        }

        public long LongCount(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().LongCount(predicate);
        }

        public long LongCount(Func<T, bool> predicate)
        {
            return _context.Set<T>().LongCount(predicate);
        }

        public IEnumerable<T> All(bool AsNoTracking = true)
        {
            if (AsNoTracking)
                return _context.Set<T>().ToList();
            else
                return _context.Set<T>().AsNoTracking().ToList();

        }

        public IQueryable<T> AsNoTracking()
        {
            return _context.Set<T>().AsQueryable<T>().AsNoTracking();
        }

        public IQueryable<T> AllIncluding(params System.Linq.Expressions.Expression<Func<T, object>>[] IncludeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in IncludeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IEnumerable<T> ExecWithStoredProcedure(string query, params object[] Parameters)
        {
            return _context.Database.SqlQuery<T>(query, Parameters).ToList();
        }

        public IEnumerable<T> ExecWithStoredProcedureNonParameters(string Query)
        {
            return _context.Database.SqlQuery<T>(Query).ToList();
        }

        public string ExecScalarWithStoredProcedure(string query, params object[] Parameters)
        {
            return _context.Database.SqlQuery<string>(query, Parameters).FirstOrDefault();
        }

        public void ExecWithStoredProcedureWithNoReturn(string query, params object[] Parameters)
        {
            _context.Database.ExecuteSqlCommand(query, Parameters);
        }

        public int ExecStoredProcedureWithRowsAffected(string query, params object[] Parameters)
        {
            return _context.Database.ExecuteSqlCommand(query, Parameters);

        }

        public int ExecuteSql(string Sql)
        {
            DbConnection conn = _context.Database.Connection;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = Sql;
                    return cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                conn.Close();
                MySqlConnection.ClearAllPools();
            }
        }


        public async Task<IEnumerable<T>> ExecWithStoredProcedureAsync(string query, params object[] Parameters)
        {
            return await _context.Database.SqlQuery<T>(query, Parameters).ToListAsync();
        }

        public async Task<IEnumerable<T>> ExecWithStoredProcedureNonParametersAsync(string Query)
        {
            return await _context.Database.SqlQuery<T>(Query).ToListAsync();
        }

        public async Task<string> ExecScalarWithStoredProcedureAsync(string query, params object[] Parameters)
        {
            return await _context.Database.SqlQuery<string>(query, Parameters).FirstOrDefaultAsync();
        }

        public async Task<int> ExecWithStoredProcedureWithNoReturnAsync(string query, params object[] Parameters)
        {
            return await _context.Database.ExecuteSqlCommandAsync(query, Parameters);
        }

        public async Task<int> ExecStoredProcedureWithRowsAffectedAsync(string query, params object[] Parameters)
        {
            return await _context.Database.ExecuteSqlCommandAsync(query, Parameters);
        }

        public async Task<int> ExecuteSqlAsync(string Sql)
        {
            DbConnection conn = _context.Database.Connection;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = Sql;
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
            finally
            {
                conn.Close();
                MySqlConnection.ClearAllPools();
            }
        }

        public async Task<T> AddAsync(T Entity)
        {
            _context.Set<T>().Add(Entity);
            await _context.SaveChangesAsync();
            return Entity;
        }

        public async Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                _context.Set<T>().Add(entity);

            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<T> UpdateAsync(T Entity)
        {
            _context.Entry<T>(Entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Entity;
        }

        public async Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                _context.Entry<T>(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<int> DeleteAsync(T Entity)
        {
            _context.Entry<T>(Entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(object KeyValue)
        {
            _context.Entry<T>(await _context.Set<T>().FindAsync(KeyValue)).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            var entities = await _context.Set<T>().Where(Expression).ToListAsync<T>();
            foreach (var entity in entities)
            {
                _context.Entry<T>(entity).State = EntityState.Deleted;
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<T> FindByIdAsync(object KeyValue)
        {
            return await _context.Set<T>().FindAsync(KeyValue);
        }

        public async Task<T> FirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(Expression);
        }

        public async Task<IEnumerable<T>> AllAsync(bool AsNoTracking = true)
        {
            if (AsNoTracking)
                return await _context.Set<T>().ToListAsync<T>();
            else
                return await _context.Set<T>().AsNoTracking<T>().ToListAsync<T>();
        }

        public async Task<IEnumerable<T>> WhereAsync(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            return await _context.Set<T>().Where(Expression).ToListAsync<T>();
        }

        public async Task<IEnumerable<T>> WhereAsync(System.Linq.Expressions.Expression<Func<T, bool>> Predicate, System.Linq.Expressions.Expression<Func<T, string>> Order)
        {
            return await _context.Set<T>().Where(Predicate).OrderBy(Order).ToListAsync<T>();
        }

        public async Task<PagedList<T>> PaginateAsync(IQueryable<T> query, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return  new PagedList<T>(query, pageIndex, pageSize);
        }

        public async Task<IEnumerable<T>> GetAsync(IQueryable<T> Queryable)
        {
            return await Queryable.ToListAsync<T>();
        }

        public async Task<IEnumerable<T>> SelectAsync(System.Linq.Expressions.Expression<Func<T, int, T>> selector)
        {
            return await _context.Set<T>().Select(selector).ToListAsync<T>();
        }

        public async Task<IEnumerable<T>> SelectManyAsync(System.Linq.Expressions.Expression<Func<T, IEnumerable<T>>> selector)
        {
            return await _context.Set<T>().SelectMany(selector).ToListAsync<T>();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().CountAsync(predicate);
        }

        public async Task<long> LongCountAsync()
        {
            return await _context.Set<T>().LongCountAsync();
        }

        public async Task<long> LongCountAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().LongCountAsync(predicate);
        }
    }
}
