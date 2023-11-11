using Contratos.Dominio.Enumeradores;
using Contratos.Dominio.ValueObjects.Moedas;
using EDSCore;

namespace Contratos.Dominio.ValueObjects
{
    public record UnidadeDeValor : ValueObject
    {
        public UnidadeDeValor(TipoContratacao tipo, ValorEmReais valor)
        {
            Tipo = tipo;
            Valor = valor;
        }

        public TipoContratacao Tipo { get; private set; }
        public ValorEmReais Valor { get; private set; }
    }
}
