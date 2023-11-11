using ContaDTOs;
using EDSCore;
using MediatR;

namespace Conta.Dominio.Commands
{
    public class IniciaContaCommand : IRequest<Resultado<ContaDOC, ValidationFalhas>>
    {
        public IniciaContaCommand(string idCustomer, string tipo)
        {
            IdCustomer = idCustomer;
            Tipo = tipo;
        }

        public string IdCustomer { get; private set; }
        public string Tipo { get; private set; }
    }
}
