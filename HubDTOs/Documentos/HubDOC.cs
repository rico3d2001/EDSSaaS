using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace HubDTOs.Documentos
{
    public class HubDOC
    {
        [BsonElement("Id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Email { get; set; }
        public List<CustomerDOC> Customers { get; set; } = new List<CustomerDOC>();
        public void AddCustomerDOC(CustomerDOC customerDOC)
        {
            Customers.Add(customerDOC);
        }
    }

    

}
