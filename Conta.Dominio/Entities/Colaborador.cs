using Conta.Dominio.Interfaces;
using Conta.Dominio.ValueObjects;
using Conta.Dominio.ValueObjects.Ids;
using EDSCore;

namespace Conta.Dominio.Entities
{
   
    public class Colaborador : Entidade<IdColaborador>
    {
        

        public Colaborador(IdColaborador id) : base(id)
        {
        }

        public Email Email { get; private set; }
        public CPF CPF { get; private set; }
        public Nome Nome { get; private set; }

        public void Iniciar(Email email, IUnitOfWorkConta unitOfWorkDomain) 
        {
            Email = email;
            //Salvar primeira vez
        }
    }
}
