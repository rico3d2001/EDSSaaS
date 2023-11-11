using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrganizacaoDTOS
{
    public class OrganizacaoDOC
    {
        [BsonElement("Id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string IdConta { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string LogoMarca { get; set; }
    }
}
