using HubDTOs.Documentos;
using Hubs.Dominio.Entities;

namespace Hubs.Dominio.Interfaces
{
    public interface IRoleCustomerRepo
    {
        Task<RoleCustomerDOC> GetByNome(string role);
        Task CreateAsync(RoleCustomerDOC data);
    }
}
