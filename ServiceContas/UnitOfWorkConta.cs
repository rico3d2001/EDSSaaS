using Conta.Dominio.Interfaces;
using Conta.Dominio.IRepositorios;
using ContaDTOs;
using RepoConta;
using RepoGenerico;

namespace ServiceContas
{
    public class UnitOfWorkConta : IUnitOfWorkConta, IDisposable
    {
        private IMongoDBContextEDS _contexto;
        private IRepoConta _repoConta;

     

        public UnitOfWorkConta(IMongoDBContextEDS mongoDBContext)
        {
            _contexto = mongoDBContext;

        }


        public IRepoConta RepoConta 
        {
            get
            {
                if (_repoConta == null)
                {
                    var collection = _contexto.Database.GetCollection<ContaDOC>("Contas");
                    _repoConta = new ContaRepo(collection);
                }
                return _repoConta;
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


