using EDSCore;

namespace Conta.Dominio.ValueObjects
{
    //public class TipoConta : Enumeration
    //{
    //    public static TipoConta Free = new TipoConta(1, nameof(Free));
    //    public static TipoConta Standard = new TipoConta(2, nameof(Standard));
    //    public static TipoConta Corporate = new TipoConta(3, nameof(Corporate));

    //    public TipoConta(int id, string name) : base(id, name)
    //    {
    //    }
    //}
    public record TipoConta : ValueObject
    {
        private TipoConta()
        {
            
        }
        public TipoConta(string value)
        {
            Texto = value.Trim();
        }
        public string Texto { get; private set; }

    }
}
