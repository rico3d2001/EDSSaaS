using EDSCore;
using Hubs.Dominio.ValueObjects;

namespace Hubs.Dominio.Entities
{
    public record IdCustomer : AbsID
    {
        private IdCustomer()
        {

        }
        public IdCustomer(string id) : base(id)
        {
        }
    }

    public class Customer : Entidade<IdCustomer>
    {
       

       

        public Customer(IdCustomer id) : base(id)
        {
            
        }

        

       

        public CPF CPF { get; private set; }

        public Credencial Credencial { get; private set; }

        public WhatsApp WhatsApp { get; private set; }
      
        public StatusCustomerType Status { get; private set; }
        



        


    }
}
