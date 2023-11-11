using EDSCore;
using System.Text.RegularExpressions;

namespace Conta.Dominio.ValueObjects
{
    
    public record Nome : ValueObject
    {
        public Nome(string texto)
        {
            Texto = texto;
        }

        public string Texto { get; private set; }
    }
}
