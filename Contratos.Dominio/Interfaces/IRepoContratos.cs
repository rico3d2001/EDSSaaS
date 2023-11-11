using ContratoDTOs;

namespace Contratos.Dominio.Interfaces
{
    public interface IRepoContratos
    {
        Task Salvar(ContratoDOC contrato);

        Task<List<ContratoDOC>> ObterContratos(string idOrganizacao);
    }
}
