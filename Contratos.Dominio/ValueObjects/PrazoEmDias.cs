using EDSCore;

namespace Contratos.Dominio.ValueObjects
{
    public record PrazoEmDias : ValueObject
    {
        public PrazoEmDias(int dias)
        {
            Dias = dias;
        }

        public int Dias { get; private set; }
    }
}
