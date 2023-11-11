using Conta.Dominio.ValueObjects;

namespace Conta.Dominio.Entities.CalculosConta
{
    public interface ICalculaConta
    {
        Dinheiro GetValorAhPagar(int qtdColaboradores);
    }
}
