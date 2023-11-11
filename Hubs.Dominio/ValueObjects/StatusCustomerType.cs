using EDSCore;

namespace Hubs.Dominio.ValueObjects
{
    public record StatusCustomerType : ValueObject
    {
        private StatusCustomerType()
        {

        }

        public StatusCustomerType(string value)
        {
            Texto = value.Trim();
        }
        public string Texto { get; private set; }

    }
}
