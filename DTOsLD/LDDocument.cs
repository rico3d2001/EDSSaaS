using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DTOsLD
{
    public class LDDocument
    {
        [BsonElement("Id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string IdProjeto { get; set; }

    }
}
