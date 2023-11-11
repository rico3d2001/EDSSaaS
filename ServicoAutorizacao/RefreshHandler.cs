using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ServicoAutorizacao.Configs;
using System.Security.Cryptography;

namespace ServicoAutorizacao
{
    public class RefreshHandler : IRefreshHandler
    {
        private readonly IMongoCollection<DocRefreshToken> _refreshTokenCollection;
        public RefreshHandler(IOptions<HubDbConfig> authDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                authDatabaseSettings.Value.Connection);

            var mongoDatabase = mongoClient.GetDatabase(
                authDatabaseSettings.Value.DatabaseName);

            _refreshTokenCollection = mongoDatabase.GetCollection<DocRefreshToken>(
                authDatabaseSettings.Value.RefreshTokensCollectionName);
        }
        public async Task<string> GenerateToken(string username)
        {
            var randomnumber = new byte[32];
            using (var randomnumbergenerator = RandomNumberGenerator.Create())
            {
                randomnumbergenerator.GetBytes(randomnumber);
                string refreshtoken = Convert.ToBase64String(randomnumber);
                var existToken = await _refreshTokenCollection.Find(x => x.UserId == username).FirstOrDefaultAsync();
                if (existToken != null)
                {
                    existToken.RefreshToken = refreshtoken;
                    var data = new DocRefreshToken()
                    {
                        Id = existToken.Id,
                        UserId = username,
                        RefreshToken = refreshtoken,
                        TokenId = existToken.Id
                    };
                    await _refreshTokenCollection.ReplaceOneAsync(x => x.UserId == username, data);
                }
                else
                {
                    await _refreshTokenCollection.InsertOneAsync(new DocRefreshToken
                    {
                        UserId = username,
                        TokenId = new Random().Next().ToString(),
                        RefreshToken = refreshtoken
                    });
                }


                return refreshtoken;
            }
        }
    }
}
