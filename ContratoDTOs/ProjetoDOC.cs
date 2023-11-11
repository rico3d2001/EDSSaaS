using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ContratoDTOs
{
    public class ProjetosDOC
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string NomeCompleto { get; set; }
        public string NumeroEDS { get; set; }
        public string NumeroCliente { get; set; }
    }
}
