using EDSCore;
using HubDTOs.Documentos;
using Hubs.Dominio.Agregados;
using Hubs.Dominio.Commands;
using Hubs.Dominio.Interfaces;
using MediatR;

namespace ServiceHub.Handlers
{
    public class ConfirmaCustomerHandler : IRequestHandler<ConfirmaCustomerCommand, Resultado<HubDOC, ValidationFalhas>>
    {
        private readonly IUnitOfWorkHub _unitOfWork;
        public ConfirmaCustomerHandler(IUnitOfWorkHub unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Resultado<HubDOC, ValidationFalhas>> Handle(ConfirmaCustomerCommand command, CancellationToken cancellationToken)
        {
            var erros = new List<ValidationFalha>();
            try
            {
                HubAgregate hub = await HubAgregate.ConfirmaCustomer(command, _unitOfWork);
                return hub.DTO;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
