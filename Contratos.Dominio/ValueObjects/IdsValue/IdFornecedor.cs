namespace Contratos.Dominio.ValueObjects.IdsValue
{
    public class IdFornecedor
    {
        public IdFornecedor(string id)
        {
            MongoGuid = id;
        }

        public string MongoGuid { get; private set; }
    }
}
