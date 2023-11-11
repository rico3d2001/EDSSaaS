using EDSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conta.Dominio.ValueObjects
{
    
    //public class StatusCustomerType : Enumeration
    //{
    //    public static StatusCustomerType Iniciado = new StatusCustomerType(1, nameof(Iniciado));
    //    public static StatusCustomerType Registrado = new StatusCustomerType(2, nameof(Registrado));
    //    public static StatusCustomerType Completo = new StatusCustomerType(3, nameof(Completo));

    //    public StatusCustomerType(int id, string name) : base(id, name)
    //    {
    //    }
    //}

    public record StatusConta : ValueObject
    {
        private StatusConta()
        {

        }

        public StatusConta(string value)
        {
            Texto = value.Trim();
        }
        public string Texto { get; private set; }

    }
}
