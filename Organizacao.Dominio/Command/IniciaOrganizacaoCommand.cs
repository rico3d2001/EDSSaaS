using EDSCore;
using MediatR;
using OrganizacaoDTOS;

namespace Organizacao.Dominio.Command
{
    public class IniciaOrganizacaoCommand : IRequest<Resultado<OrganizacaoDOC, ValidationFalhas>>
    {

        public string IdConta { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Logomarca { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;

    }
}
