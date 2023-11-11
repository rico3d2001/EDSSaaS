using EDSCore;

namespace Contratos.Dominio.ValueObjects
{
    public record Email : ValueObject
    {
        public Email(string texto)
        {
            Texto = texto;
        }

        public string Texto { get; private set; }
    }
}
