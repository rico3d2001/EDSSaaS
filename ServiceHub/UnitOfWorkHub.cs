using HubDTOs.Documentos;
using Hubs.Dominio.Interfaces;
using RepoGenerico;
using RepoHubs;

namespace ServiceHub
{
    public class UnitOfWorkHub : IUnitOfWorkHub, IDisposable
    {
        private IMongoDBContextEDS _contexto;
        private IHubRepo _hubRepo;
        private IRoleCustomerRepo _roleCustomerRepo;
        private IRegexRepo _regexRepo;
      

        public UnitOfWorkHub(IMongoDBContextEDS mongoDBContext)
        {
            _contexto = mongoDBContext;
        }
        public IHubRepo HubRepositorio
        {
            get
            {
                if (_hubRepo == null)
                {
                    var collection = _contexto.Database.GetCollection<HubDOC>("Hub");
                    _hubRepo = new HubRepo(collection);
                }
                return _hubRepo;
            }
        }

        public IRoleCustomerRepo RoleCustomerRepo
        {
            get
            {
                if (_roleCustomerRepo == null)
                {
                    var collection = _contexto.Database.GetCollection<RoleCustomerDOC>("RoleCustomer");
                    _roleCustomerRepo = new RoleCustomerRepo(collection);
                }
                return _roleCustomerRepo;
            }
        }

        public IRegexRepo RegexRepo
        {
            get
            {
                if (_regexRepo == null)
                {
                    var collection = _contexto.Database.GetCollection<RegexDOC>("Regexs");
                    _regexRepo = new RegexRepo(collection);
                }
                return _regexRepo;
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
