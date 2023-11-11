using OrganizacaoDTOS;

namespace Organizacao.Dominio.Interfaces
{
    public interface IRepoOrganizacao
    {
        Task Salvar(OrganizacaoDOC conta);

        Task<List<OrganizacaoDOC>> ObterOrganizacoes(string idConta);
    }
}
