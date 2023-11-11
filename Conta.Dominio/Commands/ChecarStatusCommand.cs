using ContaDTOs;
using EDSCore;
using MediatR;

namespace Conta.Dominio.Commands
{
    public class ChecarStatusCommand : IRequest<Resultado<ContaDOC, ValidationFalhas>>
    {
        public ChecarStatusCommand(string idCustomer)
        {
            IdCustomer = idCustomer;
        }

        public string IdCustomer { get; private set; }
    }
}
