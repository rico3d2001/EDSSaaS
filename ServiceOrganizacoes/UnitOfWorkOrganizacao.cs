using Organizacao.Dominio.Interfaces;
using OrganizacaoDTOS;
using RepoGenerico;
using RepoOrganizacao;

namespace ServiceOrganizacoes
{
    public class UnitOfWorkOrganizacao : IUnitOfWorkOrganizacao, IDisposable
    {

        private IMongoDBContextEDS _contexto;
        private IRepoOrganizacao _repoOrganizacao;



        public UnitOfWorkOrganizacao(IMongoDBContextEDS mongoDBContext)
        {
            _contexto = mongoDBContext;
        }

        public IRepoOrganizacao RepoOrganizacao
        {
            get
            {
                if (_repoOrganizacao == null)
                {
                    var collection = _contexto.Database.GetCollection<OrganizacaoDOC>("Organizacoes");
                    _repoOrganizacao = new OrganizacaoRepo(collection);
                }
                return _repoOrganizacao;
            }
        }

        #region Dispose

        private bool disposed = false;

 
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _contexto.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
