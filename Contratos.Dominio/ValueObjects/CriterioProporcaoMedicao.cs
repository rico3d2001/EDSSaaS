using Contratos.Dominio.ValueObjects.Moedas;
using EDSCore;

namespace Contratos.Dominio.ValueObjects
{
    public record CriterioProporcaoMedicao : ValueObject
    {
        public CriterioProporcaoMedicao(int indice, decimal percentual, ValorEmReais valor)
        {
            Indice = indice;
            Percentual = percentual;
            Valor = valor;
        }

        public int Indice { get; private set; }
        public decimal Percentual { get; private set; }
        public ValorEmReais Valor { get; private set; }
    }
}
