using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContaDTOs
{
    public class ContaDOC 
    {
        [BsonElement("Id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string IdCustomer { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }

    }

}

