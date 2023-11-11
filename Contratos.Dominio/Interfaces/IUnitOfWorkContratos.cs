namespace Contratos.Dominio.Interfaces
{
    public interface IUnitOfWorkContratos
    {
        IRepoContratos RepoContratos { get; }
        IRepoValidator RepoValidator { get; }
    }
}
