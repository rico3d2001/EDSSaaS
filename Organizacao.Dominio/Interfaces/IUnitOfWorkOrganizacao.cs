namespace Organizacao.Dominio.Interfaces
{
    public interface IUnitOfWorkOrganizacao
    {
        IRepoOrganizacao RepoOrganizacao { get; }
    }
}
