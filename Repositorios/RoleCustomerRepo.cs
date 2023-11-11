using HubDTOs.Documentos;
using Hubs.Dominio.Interfaces;
using MongoDB.Driver;

namespace RepoHubs
{
    public class RoleCustomerRepo : IRoleCustomerRepo
    {
        private readonly IMongoCollection<RoleCustomerDOC> _collection;
        public RoleCustomerRepo(IMongoCollection<RoleCustomerDOC> collection)
        {
            _collection = collection;
        }
        public async Task CreateAsync(RoleCustomerDOC role)
        {
            try
            {

                await _collection.InsertOneAsync(role);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<RoleCustomerDOC> GetByNome(string role)
        {
            throw new NotImplementedException();
        }
    }
}
