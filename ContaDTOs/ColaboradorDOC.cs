using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ContaDTOs
{
    public class ColaboradorDOC 
    {
        [BsonElement("Id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Sigla { get; set; }



    }


}
