using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HubDTOs.Documentos
{
    public class RegexDOC
    {
        [BsonElement("Id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ClassName { get; set; }
        public string Projeto { get; set; }
        public string Regex { get; set; }
    }
}
