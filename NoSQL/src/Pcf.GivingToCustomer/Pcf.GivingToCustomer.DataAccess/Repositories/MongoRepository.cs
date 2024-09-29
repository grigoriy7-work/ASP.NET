using Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Pcf.GivingToCustomer.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Pcf.GivingToCustomer.DataAccess.Repositories
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
        public async Task<IEnumerable<T>> GetRangeByIdsAsync(List<Guid> ids)
        {
            var entities = await _collection.FindAsync(x => ids.Contains(x.Id));
            return await entities.ToListAsync();
        }

        public async Task<T> GetFirstWhere(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await (await _collection.FindAsync(predicate)).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }
        public async Task UpdateAsync(T entity)
        {
            await _collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await _collection.FindOneAndDeleteAsync(x => x.Equals(entity));
        }     

    }
}