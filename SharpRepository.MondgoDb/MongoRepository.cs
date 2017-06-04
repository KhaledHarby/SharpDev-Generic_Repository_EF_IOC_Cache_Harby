using SharpDev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver.Builders;

namespace SharpRepository.MongoDb
{
    public partial class MongoRepository<T> : IRepository<T> where T : class
    {
        protected internal MongoCollection<T> collection;
        private MongoClient _client;

        
        public MongoRepository(string connectionString, string databaseName, string collectionName)
        {
            _client = new MongoClient(connectionString);
            collection = _client.GetServer().GetDatabase(databaseName).GetCollection<T>(collectionName);
        }

        public T Add(T Entity)
        {
            this.collection.Insert<T>(Entity);
            return Entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            this.collection.InsertBatch<T>(entities);
            return entities;
        }

        public T Update(T Entity)
        {
            this.collection.Save<T>(Entity);
            return Entity;
        }

        public IEnumerable<T> Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                this.collection.Save<T>(entity);
            }
            return entities;
        }

        public void Delete(T Entity)
        {
        }

        public void Delete(object KeyValue)
        {
            this.collection.Remove(Query.EQ("_id", KeyValue.ToString()));
        }

        public void Delete(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            throw new NotImplementedException();
        }

        public T FindById(object KeyValue)
        {
            return this.collection.FindOneByIdAs<T>(KeyValue.ToString());
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
            return this.collection.AsQueryable<T>();
        }

        public IEnumerable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> Expression)
        {
            return collection.AsQueryable<T>().Where(Expression).ToList<T>();
        }

        public IEnumerable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> Predicate, System.Linq.Expressions.Expression<Func<T, string>> Order)
        {
            return collection.AsQueryable<T>().Where(Predicate).OrderBy(Order).ToList<T>();
        }
      
        public PagedList<T> Paginate(IQueryable<T> query, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return new PagedList<T>(query, pageIndex, pageSize);
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Select(System.Linq.Expressions.Expression<Func<T, int, T>> selector)
        {
            return collection.AsQueryable<T>().Select(selector).ToList<T>();
        }

        public IEnumerable<T> SelectMany(System.Linq.Expressions.Expression<Func<T, IEnumerable<T>>> selector)
        {
            return collection.AsQueryable<T>().SelectMany(selector).ToList<T>();

        }

        public int Count()
        {
            return (int)collection.Count();
        }

        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return (int)collection.AsQueryable<T>().Where(predicate).Count();
        }

        public int Count(Func<T, bool> predicate)
        {
            return (int)collection.AsQueryable<T>().Where(predicate).Count();
        }

        public long LongCount()
        {
            return collection.AsQueryable<T>().Count();

        }

        public long LongCount(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return collection.AsQueryable<T>().Where(predicate).Count();
        }

        public long LongCount(Func<T, bool> predicate)
        {
            return collection.AsQueryable<T>().Where(predicate).Count();
        }
    }
}
