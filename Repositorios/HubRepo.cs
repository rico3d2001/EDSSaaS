using HubDTOs.Documentos;
using Hubs.Dominio.Agregados;
using Hubs.Dominio.Interfaces;
using MongoDB.Driver;

namespace RepoHubs
{
    public class HubRepo : IHubRepo
    {

        private readonly IMongoCollection<HubDOC> _collection;

        public HubRepo(IMongoCollection<HubDOC> collection)
        {
            _collection = collection;
        }

        public async Task AddContaBase(HubAgregate hub)
        {
            throw new NotImplementedException();
        }

        public async Task Confirmar(HubDOC hubEncontrado)
        {
            try
            {
                //hubEncontrado.Customers.Remove(hubEncontrado.Customers.First(x => x.Id == hubEncontrado.Customers.First().Id));

                await _collection.ReplaceOneAsync(x => x.Id == hubEncontrado.Id, hubEncontrado);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HubDOC> GetByEmail(string email)
        {
            try
            {
                return await _collection.Find(x => x.Email == email).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<HubDOC> GetById(string id)
        {
            try
            {
                return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Iniciar(HubDOC hub)
        {
            try
            {
                await _collection.InsertOneAsync(hub);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
