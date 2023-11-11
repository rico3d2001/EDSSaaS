using HubDTOs.Documentos;

namespace Hubs.Dominio.Interfaces
{
    public interface IRegexRepo
    {
        Task Salvar(RegexDOC regex);
        Task<RegexDOC> ObterUm(string projeto, string className);
    }
}
