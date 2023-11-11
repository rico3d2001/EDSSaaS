using MongoDB.Driver;
using Organizacao.Dominio.Interfaces;
using OrganizacaoDTOS;

namespace RepoOrganizacao
{
    public class OrganizacaoRepo : IRepoOrganizacao
    {
        private readonly IMongoCollection<OrganizacaoDOC> _collection;
     

        public OrganizacaoRepo(IMongoCollection<OrganizacaoDOC> collection)
        {
            _collection = collection;
        }

        public async Task<List<OrganizacaoDOC>> ObterOrganizacoes(string idConta)
        {
            var organizacoesEncontaradas = await _collection.FindAsync(x => x.IdConta == idConta);
            return organizacoesEncontaradas.ToList();
        }

        public async Task Salvar(OrganizacaoDOC organizacao)
        {
            try
            {
                await _collection.InsertOneAsync(organizacao);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

       
    }
}
