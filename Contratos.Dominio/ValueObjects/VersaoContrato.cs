using EDSCore;

namespace Contratos.Dominio.ValueObjects
{
    public record VersaoContrato : ValueObject
    {
        public VersaoContrato(int indice, DateTime data)
        {
            Indice = indice;
            Data = data;
        }

        public int Indice { get; private set; }
        public DateTime Data { get; private set; }
    }
}
