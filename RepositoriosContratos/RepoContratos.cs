using ContratoDTOs;
using Contratos.Dominio.Interfaces;
using MongoDB.Driver;

namespace Repositorios
{
    public class RepoContratos : IRepoContratos
    {
        private readonly IMongoCollection<ContratoDOC> _collection;

        public RepoContratos(IMongoCollection<ContratoDOC> collection)
        {
            _collection = collection;
        }
        public async Task<List<ContratoDOC>> ObterContratos(string idOrganizacao)
        {
            var contratosEncontrados = await _collection.FindAsync(x => x.IdOrganizacao == idOrganizacao);
            return contratosEncontrados.ToList();
        }

        public async Task Salvar(ContratoDOC contrato)
        {
            try
            {
                await _collection.InsertOneAsync(contrato);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
