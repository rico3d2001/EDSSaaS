using EDSCore;

namespace Conta.Dominio.ValueObjects
{
    public record Dinheiro : ValueObject
    {
        private Dinheiro() { }
        public Dinheiro(string moeda, decimal valor)
        {
            Moeda = moeda;
            Valor = valor;
            Texto = $"{Moeda}{Valor}";
        }

        public string Moeda { get; private set; }
        public decimal Valor { get; private set; }
        public string Texto { get; private set; }


    }
}
