using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Pagos.Infrastructure.MongoDB;
using Pagos.Infrastructure.MongoDB.Documents;

namespace Pagos.Infrastructure.Mongo
{
    public class MongoDbContext : IPaymentMongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoSettings> options)
        {
            var settings = options.Value;
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<PaymentDocument> Payment =>
            _database.GetCollection<PaymentDocument>("payment");

        public IMongoDatabase Database => _database;
    
    }
}
