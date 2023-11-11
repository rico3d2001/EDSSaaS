using ContratoDTOs;
using Contratos.Dominio.Interfaces;
using RepoGenerico;
using Repositorios;

namespace ServiceContratos
{
    public class UnitOfWorkContratos : IUnitOfWorkContratos, IDisposable
    {

        private IMongoDBContextEDS _contexto;
        private IRepoContratos _repoContratos;
        private IRepoValidator _repoValidator;
        public UnitOfWorkContratos(IMongoDBContextEDS mongoDBContext)
        {
            _contexto = mongoDBContext;
        }

       
        public IRepoContratos RepoContratos
        {
            get
            {
                if (_repoContratos == null)
                {
                    var collection = _contexto.Database.GetCollection<ContratoDOC>("Contratos");
                    _repoContratos = new RepoContratos(collection);
                }
                return _repoContratos;
            }
        }

        public IRepoValidator RepoValidator
        {
            get
            {
                if (_repoValidator == null)
                {
                    var collection = _contexto.Database.GetCollection<ValidatorDOC>("Validators");
                    _repoValidator = new RepoValidators(collection);
                }
                return _repoValidator;
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
