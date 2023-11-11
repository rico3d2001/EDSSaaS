using EDSCore;

namespace Contratos.Dominio.ValueObjects.IdsValue
{
    public record IdContrato : ValueObject
    {
        public IdContrato(string id)
        {
            MongoGuid = id;
        }

        public string MongoGuid { get; private set; }
    }
}
