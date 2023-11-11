using ContratoDTOs;
using Contratos.Dominio.Interfaces;
using MongoDB.Driver;

namespace Repositorios
{
    public class RepoValidators : IRepoValidator
    {
        private readonly IMongoCollection<ValidatorDOC> _collection;

        public RepoValidators(IMongoCollection<ValidatorDOC> collection)
        {
            _collection = collection;
        }

        public Task<ValidatorDOC> ObterValidator(string idOrganizacao, string idProjeto, string tipo)
        {
            throw new NotImplementedException();
        }

        public Task Salvar(ValidatorDOC contrato)
        {
            throw new NotImplementedException();
        }
    }
}
