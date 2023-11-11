namespace Hubs.Dominio.Interfaces
{
    public interface IUnitOfWorkHub
    {
        IHubRepo HubRepositorio { get; }
        IRoleCustomerRepo RoleCustomerRepo { get; }
        IRegexRepo RegexRepo { get; }

    }
}
