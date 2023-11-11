using ContratoDTOs;

namespace Contratos.Dominio.Interfaces
{
    public interface IRepoValidator
    {
        Task Salvar(ValidatorDOC contrato);

        Task<ValidatorDOC> ObterValidator(string idOrganizacao, string idProjeto, string tipo);
    }
}
