using HubDTOs.Documentos;
using Hubs.Dominio.Agregados;

namespace Hubs.Dominio.Interfaces
{
    public interface IHubRepo
    {
        Task Iniciar(HubDOC hub);
        Task Confirmar(HubDOC hub);
        Task<HubDOC> GetByEmail(string email);
        Task<HubDOC> GetById(string id);
        Task AddContaBase(HubAgregate hub);

    }
}
