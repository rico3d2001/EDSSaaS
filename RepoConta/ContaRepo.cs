using Conta.Dominio.IRepositorios;
using ContaDTOs;
using MongoDB.Driver;

namespace RepoConta
{
    public class ContaRepo : IRepoConta
    {
        private readonly IMongoCollection<ContaDOC> _collection;

        public ContaRepo(IMongoCollection<ContaDOC> collection)
        {
            _collection = collection;
        }

        public async Task<ContaDOC> ObterUltimaContaPorCustomer(string idCustomer)
        {
            var contasEncontaradas = await _collection.FindAsync(x => x.IdCustomer == idCustomer);
            return contasEncontaradas.ToList().LastOrDefault();
        }

        public async Task Salvar(ContaDOC conta)
        {
            try
            {
                await _collection.InsertOneAsync(conta);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
