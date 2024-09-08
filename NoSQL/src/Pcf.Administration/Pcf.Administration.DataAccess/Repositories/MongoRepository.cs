using Pcf.Administration.Core.Abstractions.Repositories;
using Pcf.Administration.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Pcf.Administration.DataAccess.Repositories
{
    public class MongoRepository<T>
        : IRepository<T>
        where T : BaseEntity
    {
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<T> _collection;   

        public MongoRepository(IMongoDatabase db)
        {
            _db = db;
            _collection = db.GetCollection<T>(typeof(T).Name);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _collection.Find("{}").ToListAsync();
            return entities;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return entity;
        }
        public Task<IEnumerable<T>> GetRangeByIdsAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetFirstWhere(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }
        public async Task UpdateAsync(T entity)
        {
            await _collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity);
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }     

    }
}