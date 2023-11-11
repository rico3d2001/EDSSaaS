using EDSCore;

namespace Contratos.Dominio.ValueObjects.Moedas
{
    public record ValorEmReais : ValueObject
    {
        public ValorEmReais(decimal valor)
        {
            Valor = valor;
            Moeda = "R$";
        }

        public decimal Valor { get; private set; }
        public string Moeda { get; private set; }
    }
}
