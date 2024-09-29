using MongoDB.Driver;
using Pcf.GivingToCustomer.Core.Domain;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.DataAccess.Data
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
            //_dataContext.Database.EnsureDeleted();
            //_dataContext.Database.EnsureCreated();

            //_dataContext.AddRange(FakeDataFactory.Preferences);
            //_dataContext.SaveChanges();

            //_dataContext.AddRange(FakeDataFactory.Customers);
            //_dataContext.SaveChanges();

            _db.DropCollection(typeof(Preference).Name);
            var collectionPreferences = _db.GetCollection<Preference>(typeof(Preference).Name);
            collectionPreferences.InsertMany(FakeDataFactory.Preferences);

            _db.DropCollection(typeof(Customer).Name);
            var collectionCustomers = _db.GetCollection<Customer>(typeof(Customer).Name);
            collectionCustomers.InsertMany(FakeDataFactory.Customers);
        }
    }
}