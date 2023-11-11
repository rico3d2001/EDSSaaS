using EDSCore;

namespace Conta.Dominio.ValueObjects.Ids
{

    public record IdConta : ValueObject
    {
        public IdConta(string id)
        {
            MongoGuid = id;
        }

        public string MongoGuid { get; private set; }
    }
}
