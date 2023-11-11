﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RepoGenerico;

namespace ContasAPI.Configs
{
    public class ContasDbContext : IMongoDBContextEDS, IDisposable
    {
        private IMongoDatabase _database;
        private IMongoClient _client;

        public IMongoDatabase Database { get => _database; set => _database = value; }
        public IMongoClient Client { get => _client; set => _client = value; }

        public ContasDbContext(IOptions<ContatosDbConfig> customerDatabaseSettings)
        {
            _client = new MongoClient(customerDatabaseSettings.Value.Connection);
            _database = _client.GetDatabase(customerDatabaseSettings.Value.DatabaseName);

        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
