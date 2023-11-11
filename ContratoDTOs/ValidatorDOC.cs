using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContratoDTOs
{
    public class ValidatorDOC
    {
        [BsonElement("Id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string IdOrganizacao { get; set; }
        public string IdProjeto { get; set; }
        public string Tipo { get; set; }
        public string Regex { get; set; }
    }
}
