using Conta.Dominio.Agregado;
using Conta.Dominio.Commands;
using Conta.Dominio.Interfaces;
using ContaDTOs;
using EDSCore;
using MediatR;

namespace ServiceContas.Command
{
    public class ChecarStatusHandler : IRequestHandler<ChecarStatusCommand, Resultado<ContaDOC, ValidationFalhas>>
    {
        private readonly IUnitOfWorkConta _unitOfWork;
        public ChecarStatusHandler(IUnitOfWorkConta unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Resultado<ContaDOC, ValidationFalhas>> Handle(ChecarStatusCommand request, CancellationToken cancellationToken)
        {
            var erros = new List<ValidationFalha>();
            try
            {
                var conta = await _unitOfWork.RepoConta.ObterUltimaContaPorCustomer(request.IdCustomer);
                if (conta == null)
                {
                    var contaAgregate = await ContaAgregate.Iniciar(request.IdCustomer, "Free", _unitOfWork);
                    conta = contaAgregate.DTO;
                }
                return conta;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
