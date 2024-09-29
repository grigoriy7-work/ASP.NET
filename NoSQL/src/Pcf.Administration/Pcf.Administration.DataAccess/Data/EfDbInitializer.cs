using MongoDB.Driver;
using Pcf.Administration.Core.Domain.Administration;

namespace Pcf.Administration.DataAccess.Data
{
    public class EfDbInitializer
        : IDbInitializer
    {
        private readonly DataContext _dataContext;
        private readonly IMongoDatabase _db;

        public EfDbInitializer(DataContext dataContext, IMongoDatabase db)
        {
            _dataContext = dataContext;
            _db = db;
        }
        
        public void InitializeDb()
        {
            /* _dataContext.Database.EnsureDeleted();
             _dataContext.Database.EnsureCreated();

             _dataContext.AddRange(FakeDataFactory.Employees);
             _dataContext.SaveChanges();*/
            string employeeCollectionName = typeof(Employee).Name;
            _db.DropCollection(employeeCollectionName);
            var collectionEmployee = _db.GetCollection<Employee>(employeeCollectionName);
            collectionEmployee.InsertManyAsync(FakeDataFactory.Employees);

        }
    }
}