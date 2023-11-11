using EDSCore;

namespace Contratos.Dominio.ValueObjects
{
    public record PeriodoMedicao : ValueObject
    {
        public PeriodoMedicao(DateTime inicial, DateTime final)
        {
            Inicial = inicial;
            Final = final;
        }

        public DateTime Inicial { get; set; }
        public DateTime Final { get; set; }
    }
}
