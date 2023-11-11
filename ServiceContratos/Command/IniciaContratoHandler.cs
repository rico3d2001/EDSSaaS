using ContratoDTOs;
using Contratos.Dominio.Agregados;
using Contratos.Dominio.Commands;
using Contratos.Dominio.Interfaces;
using EDSCore;
using MediatR;

namespace ServiceContratos.Command
{
    public class IniciaContratoHandler : IRequestHandler<IniciarContratoCommand, Resultado<ContratoDOC, ValidationFalhas>>
    {
        private readonly IUnitOfWorkContratos _unitOfWork;
        public IniciaContratoHandler(IUnitOfWorkContratos unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Resultado<ContratoDOC, ValidationFalhas>> Handle(IniciarContratoCommand command, CancellationToken cancellationToken)
        {
            var erros = new List<ValidationFalha>();
            try
            {

                var contrato = await ContratoAgregate.IniciarContrato(command, _unitOfWork);

                return contrato.DTO;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
