using HubDTOs.Documentos;
using Hubs.Dominio.Interfaces;
using MongoDB.Driver;

namespace RepoHubs
{
    public class RegexRepo : IRegexRepo
    {
        private readonly IMongoCollection<RegexDOC> _collection;
        public RegexRepo(IMongoCollection<RegexDOC> collection)
        {
            _collection = collection; 
        }

        public async Task<RegexDOC> ObterUm(string projeto, string className)
        {
            try
            {
                return await _collection.Find(x => x.Projeto == projeto && x.ClassName == className).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Salvar(RegexDOC regex)
        {
            try
            {
                await _collection.InsertOneAsync(regex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
