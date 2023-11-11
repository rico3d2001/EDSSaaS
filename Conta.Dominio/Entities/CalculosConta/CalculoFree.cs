using Conta.Dominio.Entities.CalculosConta;
using Conta.Dominio.ValueObjects;

namespace Conta.Dominio.Entities.ContaHierarquia
{
    public class CalculoFree : ICalculaConta
    {
        public Dinheiro GetValorAhPagar(int qtdColaboradores)
        {
            return new Dinheiro("R$", 0);
        }
    }
}
