using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HubDTOs.Documentos
{
    public class RoleCustomerDOC
    {
        [BsonElement("Id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        public string Role { get; set; }
        [Required]
        public string Menu { get; set; }
        [Required]
        public bool HaveAdd { get; set; }
        public bool HaveEdit { get; set; }
        public bool HaveMove { get; set; }
    }
}
