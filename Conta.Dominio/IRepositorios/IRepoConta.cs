using ContaDTOs;

namespace Conta.Dominio.IRepositorios
{
    public interface IRepoConta
    {
        Task Salvar(ContaDOC conta);

        Task<ContaDOC> ObterUltimaContaPorCustomer(string idCustomer);
    }
}
