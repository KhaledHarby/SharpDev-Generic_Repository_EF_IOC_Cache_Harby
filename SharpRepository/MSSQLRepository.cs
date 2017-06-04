﻿using SharpDev.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRepository.MsSql
{
    public partial class MSSQLRepository<T> : IRepository<T>, IRepositoryExtension<T>, IRepositoryAsyncExtension<T>, IRepositoryAsync<T> where T : class
    {
        private DbContext _DbContext;
        public MSSQLRepository(DbContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext is null");
            this._DbContext = dbContext;
        }
        public T Add(T Entity)
        {
            if (Entity == null) throw new ArgumentNullException("Entity is null");
            try
            {
                Entity = _DbContext.Set<T>().Add(Entity);
                _DbContext.SaveChanges();

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> Entities)
        {
            if (Entities == null) throw new ArgumentNullException("Entity is null");
            try
            {
                _DbContext.Set<T>().AddRange(Entities);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public T Update(T Entity)
        {
            if (Entity == null) throw new ArgumentNullException("Entity is null");
            try
            {
                _DbContext.Entry<T>(Entity).State = System.Data.Entity.EntityState.Modified;

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public IEnumerable<T> Update(IEnumerable<T> Entities)
        {
            if (Entities == null) throw new ArgumentNullException("Entity is null");
            try
            {
                foreach (var entity in Entities)
                {
                    _DbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
                }

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public void Delete(T Entity)
        {
            if (Entity == null) throw new ArgumentNullException("Entity is null");
            try
            {
                _DbContext.Entry<T>(Entity).State = System.Data.Entity.EntityState.Deleted;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(IEnumerable<T> Entities)
        {
            if (Entities == null) throw new ArgumentNullException("Entity is null");
            try
            {
                foreach (var entity in Entities)
                {
                    _DbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
                }

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(object KeyValue)
        {
            if (KeyValue == null) throw new ArgumentNullException("Entity is null");
            try
            {
                _DbContext.Entry(_DbContext.Set<T>().Find(KeyValue)).State = System.Data.Entity.EntityState.Deleted;

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            try
            {
                var fetchedData = _DbContext.Set<T>().AsNoTracking().Where(Expression).ToList();
                foreach (var entity in fetchedData)
                {
                    _DbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public T FindById(object KeyValue)
        {
            if (KeyValue == null) throw new ArgumentNullException("Entity is null");
            try
            {
                return _DbContext.Set<T>().Find(KeyValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T FirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            if (Expression == null) throw new ArgumentNullException("Expression is null");
            try
            {
                return _DbContext.Set<T>().AsNoTracking().FirstOrDefault(Expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T LastOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            if (Expression == null) throw new ArgumentNullException("Expression is null");
            try
            {
                return _DbContext.Set<T>().AsNoTracking().LastOrDefault(Expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<T> AsNoTracking()
        {
            return _DbContext.Set<T>().AsNoTracking<T>();
        }

        public IEnumerable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            return _DbContext.Set<T>().Where(Expression);
        }

        public IEnumerable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> Predicate, System.Linq.Expressions.Expression<Func<T, string>> Order)
        {
            return _DbContext.Set<T>().Where(Predicate).OrderBy(Order);
        }

        public IQueryable<T> AllIncluding(params System.Linq.Expressions.Expression<Func<T, object>>[] IncludeProperties)
        {
            IQueryable<T> query = _DbContext.Set<T>();
            foreach (var includeProperty in IncludeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IEnumerable<T> ExecWithStoredProcedure(string query, params object[] Parameters)
        {
            try
            {
                var resulte = _DbContext.Database.SqlQuery<T>(query, Parameters).ToList();
                return resulte;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<T> ExecWithStoredProcedureNonParameters(string Query)
        {
            try
            {
                var resulte = _DbContext.Database.SqlQuery<T>(Query).ToList();
                return resulte;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public string ExecScalarWithStoredProcedure(string query, params object[] Parameters)
        {
            try
            {
                return _DbContext.Database.SqlQuery<string>(query, Parameters).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExecWithStoredProcedureWithNoReturn(string query, params object[] Parameters)
        {
            try
            {
                _DbContext.Database.ExecuteSqlCommand(query, Parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExecStoredProcedureWithRowsAffected(string query, params object[] Parameters)
        {
            try
            {
                return _DbContext.Database.ExecuteSqlCommand(query, Parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExecuteSql(string Sql)
        {
            DbConnection conn = _DbContext.Database.Connection;
            ConnectionState initialState = conn.State;
            try
            {
                if (initialState != ConnectionState.Open)
                    conn.Open();  // open connection if not already open
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = Sql;
                    return cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (initialState != ConnectionState.Open)
                    conn.Close(); // only close connection if not initially open
            }
        }

        public PagedList<T> Paginate(IQueryable<T> query, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return new PagedList<T>(query.AsNoTracking(), pageIndex, pageSize);
        }

        public IQueryable<T> All()
        {
            return _DbContext.Set<T>().AsQueryable<T>();
        }

        public IEnumerable<T> All(bool AsNoTracking = true)
        {
            if (AsNoTracking)
                return _DbContext.Set<T>().AsNoTracking().ToList<T>();
            else
                return _DbContext.Set<T>().ToList<T>();
        }

        public int SaveChanges()
        {
            return _DbContext.SaveChanges();
        }



        public async Task<T> AddAsync(T Entity)
        {
            if (Entity == null)
                throw new Exception("Entity is null");

            _DbContext.Set<T>().Add(Entity);
            await _DbContext.SaveChangesAsync();
            return Entity;
        }

        public async Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new Exception("entities is null");

            for (int i = 0; i < entities.ToList().Count(); i++)
                _DbContext.Set<T>().Add(entities.ToList()[i]);


            await _DbContext.SaveChangesAsync();

            return entities;
        }

        public async Task<T> UpdateAsync(T Entity)
        {
            if (Entity == null)
                throw new Exception("Entity is null");

            _DbContext.Entry<T>(Entity).State = System.Data.Entity.EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return Entity;
        }

        public async Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new Exception("entities is null");

            for (int i = 0; i < entities.ToList().Count(); i++)
                _DbContext.Entry<T>(entities.ToList()[i]).State = System.Data.Entity.EntityState.Modified;

            await _DbContext.SaveChangesAsync();
            return entities;
        }

        public async Task<int> DeleteAsync(T Entity)
        {
            if (Entity == null)
                throw new Exception("Entity is null");

            _DbContext.Entry<T>(Entity).State = System.Data.Entity.EntityState.Deleted;
            return await _DbContext.SaveChangesAsync();

        }

        public async Task<int> DeleteAsync(object KeyValue)
        {
            if (KeyValue == null)
                throw new Exception("KeyValue is null");

            var entity = _DbContext.Set<T>().Find(KeyValue);

            _DbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            return await _DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            if (Expression == null)
                throw new Exception("Expression is null");

            var fetchedData = await _DbContext.Set<T>().AsNoTracking().Where(Expression).ToListAsync();
            foreach (var entity in fetchedData)
            {
                _DbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            }

            return await _DbContext.SaveChangesAsync();
        }

        public async Task<T> FindByIdAsync(object KeyValue)
        {
            if (KeyValue == null)
                throw new Exception("KeyValue is null");

            return await _DbContext.Set<T>().FindAsync(KeyValue);
        }

        public async Task<T> FirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            if (Expression == null)
                throw new Exception("Expression is null");

            return await _DbContext.Set<T>().FirstOrDefaultAsync(Expression);
        }

        public async Task<IEnumerable<T>> AllAsync(bool AsNoTracking = true)
        {
            if (AsNoTracking)
                return await _DbContext.Set<T>().AsNoTracking().ToListAsync<T>();
            else
                return await _DbContext.Set<T>().ToListAsync<T>();

        }

        public async Task<IEnumerable<T>> ExecWithStoredProcedureAsync(string query, params object[] Parameters)
        {
            return await _DbContext.Database.SqlQuery<T>(query, Parameters).ToListAsync();

        }

        public async Task<IEnumerable<T>> ExecWithStoredProcedureNonParametersAsync(string Query)
        {
            return await _DbContext.Database.SqlQuery<T>(Query).ToListAsync();
        }

        public async Task<string> ExecScalarWithStoredProcedureAsync(string query, params object[] Parameters)
        {
            return await _DbContext.Database.SqlQuery<string>(query, Parameters).FirstOrDefaultAsync();
        }

        public async Task<int> ExecWithStoredProcedureWithNoReturnAsync(string query, params object[] Parameters)
        {
            return await _DbContext.Database.ExecuteSqlCommandAsync(query, Parameters);
        }

        public async Task<int> ExecStoredProcedureWithRowsAffectedAsync(string query, params object[] Parameters)
        {
            return await _DbContext.Database.ExecuteSqlCommandAsync(query, Parameters);
        }

        public async Task<int> ExecuteSqlAsync(string Sql)
        {
            DbConnection conn = _DbContext.Database.Connection;
            ConnectionState initialState = conn.State;
            try
            {
                if (initialState != ConnectionState.Open)
                    conn.Open();  // open connection if not already open
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = Sql;
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
            finally
            {
                if (initialState != ConnectionState.Open)
                    conn.Close(); // only close connection if not initially open
            }
        }

        public async Task<PagedList<T>> PaginateAsync(IQueryable<T> query, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return new PagedList<T>(query.AsNoTracking(), pageIndex, pageSize);
        }


        public async Task<IEnumerable<T>> WhereAsync(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            return await _DbContext.Set<T>().Where(Expression).ToListAsync<T>();
        }

        public async Task<IEnumerable<T>> WhereAsync(System.Linq.Expressions.Expression<Func<T, bool>> Predicate, System.Linq.Expressions.Expression<Func<T, string>> Order)
        {
            return await _DbContext.Set<T>().Where(Predicate).OrderBy(Order).ToListAsync<T>();
        }


        public async Task<IEnumerable<T>> GetAsync(IQueryable<T> Queryable)
        {
            return await Queryable.ToListAsync<T>();
        }


        public IEnumerable<T> Select(System.Linq.Expressions.Expression<Func<T, int, T>> selector)
        {
            if (selector == null)
                throw new Exception("selector is null");

            return _DbContext.Set<T>().Select(selector).ToList();
        }


        public IEnumerable<T> SelectMany(System.Linq.Expressions.Expression<Func<T, IEnumerable<T>>> selector)
        {
            if (selector == null)
                throw new Exception("selector is null");

            return _DbContext.Set<T>().SelectMany(selector).ToList();
        }


        public async Task<IEnumerable<T>> SelectAsync(System.Linq.Expressions.Expression<Func<T, int, T>> selector)
        {
            if (selector == null)
                throw new Exception("selector is null");

            return await _DbContext.Set<T>().Select(selector).ToListAsync();
        }

        public async Task<IEnumerable<T>> SelectManyAsync(System.Linq.Expressions.Expression<Func<T, IEnumerable<T>>> selector)
        {
            if (selector == null)
                throw new Exception("selector is null");

            return await _DbContext.Set<T>().SelectMany(selector).ToListAsync();
        }


        public int Count()
        {
            return _DbContext.Set<T>().Count();
        }

        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                throw new Exception("expression is null");

            return _DbContext.Set<T>().Count(predicate);
        }

        public int Count(Func<T, bool> predicate)
        {
            if (predicate == null)
                throw new Exception("expression is null");

            return _DbContext.Set<T>().Count(predicate);
        }

        public long LongCount()
        {
            return _DbContext.Set<T>().LongCount();
        }

        public long LongCount(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                throw new Exception("expression is null");

            return _DbContext.Set<T>().LongCount(predicate);
        }

        public long LongCount(Func<T, bool> predicate)
        {
            if (predicate == null)
                throw new Exception("expression is null");

            return _DbContext.Set<T>().LongCount(predicate);
        }


        public async Task<int> CountAsync()
        {
            return await _DbContext.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                throw new Exception("expression is null");

            return await _DbContext.Set<T>().CountAsync(predicate);
        }

        public async Task<long> LongCountAsync()
        {
            return await _DbContext.Set<T>().LongCountAsync();
        }

        public async Task<long> LongCountAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                throw new Exception("expression is null");

            return await _DbContext.Set<T>().LongCountAsync(predicate);
        }
    }
}
