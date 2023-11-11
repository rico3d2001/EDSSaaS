using MongoDB.Driver;

namespace RepoGenerico
{
    public interface IMongoDBContextEDS : IDisposable
    {
        IMongoDatabase Database { get; set; }
        IMongoClient Client { get; set; }

    }
}
