using EDSCore;

namespace Organizacao.Dominio.ValueObjects
{
    public record IdOrganizacao : ValueObject
    {
        public IdOrganizacao(string id)
        {
            MongoGuid = id;
        }

        public string MongoGuid { get; private set; }
    }
}
