using Conta.Dominio.Agregado;
using Conta.Dominio.Commands;
using Conta.Dominio.Entities.CalculosConta;
using Conta.Dominio.Entities.ContaHierarquia;
using Conta.Dominio.Interfaces;
using Conta.Dominio.ValueObjects.Ids;
using ContaDTOs;
using EDSCore;
using MediatR;
using MongoDB.Bson;

namespace ServiceContas.Command
{
    public class IniciaContaHandler : IRequestHandler<IniciaContaCommand, Resultado<ContaDOC, ValidationFalhas>>
    {
        private readonly IUnitOfWorkConta _unitOfWork;

        public IniciaContaHandler(IUnitOfWorkConta unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Resultado<ContaDOC, ValidationFalhas>> Handle(IniciaContaCommand request, CancellationToken cancellationToken)
        {
            var erros = new List<ValidationFalha>();
            try
            {
                var conta = await ContaAgregate.Iniciar(request.IdCustomer, request.Tipo, _unitOfWork);
                return conta.DTO;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
