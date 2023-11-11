using EDSCore;

namespace Contratos.Dominio.ValueObjects
{
    public record SiglaProfissional : ValueObject
    {
        public SiglaProfissional(string texto)
        {
            Texto = texto;
        }

        public string Texto { get; private set; }
    }
}
