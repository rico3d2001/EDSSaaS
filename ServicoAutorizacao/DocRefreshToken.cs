using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ServicoAutorizacao
{
    public class DocRefreshToken
    {
        [BsonElement("Id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string TokenId { get; set; } = string.Empty;
    }
}
