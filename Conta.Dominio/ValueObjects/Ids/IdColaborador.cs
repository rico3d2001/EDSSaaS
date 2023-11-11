using EDSCore;

namespace Conta.Dominio.ValueObjects.Ids
{
    public record IdColaborador : ValueObject
    {
        public IdColaborador(string id)
        {
            MongoGuid = id;
        }

        public string MongoGuid { get; private set; }
    }
}
