using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HubDTOs.Documentos
{
    public class CustomerDOC
    {

        [BsonElement("Id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }


        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CPF { get; set; }
        public string? Foto { get; set; }
        public string Status { get; set; }

    }

}
