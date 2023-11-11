using EDSCore;
using MediatR;
using Organizacao.Dominio.Agregado;
using Organizacao.Dominio.Command;
using Organizacao.Dominio.Interfaces;
using OrganizacaoDTOS;

namespace ServiceOrganizacoes.Command
{


    public sealed class IniciaOrganizacaoHandler : IRequestHandler<IniciaOrganizacaoCommand, Resultado<OrganizacaoDOC, ValidationFalhas>>
    {
        private readonly IUnitOfWorkOrganizacao _unitOfWork;


        public IniciaOrganizacaoHandler(IUnitOfWorkOrganizacao unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Resultado<OrganizacaoDOC, ValidationFalhas>> Handle(IniciaOrganizacaoCommand command, CancellationToken cancellationToken)
        {
            var erros = new List<ValidationFalha>();
            try
            {

                var organizacaoAgregate = await OrganizacaoAgregate.CriarOrganizacao(command, _unitOfWork);

                return organizacaoAgregate.DTO;
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
