using Conta.Dominio.Entities.CalculosConta;
using Conta.Dominio.Entities.ContaHierarquia;
using Conta.Dominio.Interfaces;
using Conta.Dominio.ValueObjects;
using Conta.Dominio.ValueObjects.Ids;
using ContaDTOs;
using EDSCore;
using MongoDB.Bson;

namespace Conta.Dominio.Agregado
{


    public class ContaAgregate : Entidade<IdConta>, IAggregateRoot
    {
        ICalculaConta _calculosConta;

        TipoConta _tipo;
        int _qtdColaboradores;
        StatusConta _status;
        DateTime _dataCriacao;
        string _idCustomer;

        public TipoConta Tipo { get => _tipo; private set => _tipo = value; }
        public int QtdColaboradores { get => _qtdColaboradores; private set => _qtdColaboradores = value; }
        
        public StatusConta Status { get => _status; private set => _status = value; }
        public DateTime DataCriacao { get => _dataCriacao; private set => _dataCriacao = value; }
        public string IdCustomer { get => _idCustomer; private set => _idCustomer = value; }

        public ContaDOC DTO { get; set; }


        public ContaAgregate(IdConta idConta, ICalculaConta calculosConta) : base(idConta)
        {
            _calculosConta = calculosConta;
            _qtdColaboradores = 0;
        }

        public static async Task<ContaAgregate> Iniciar(string idCustomer, string tipo, IUnitOfWorkConta unitOfWork)
        {
            
            ICalculaConta calculo = new CalculoFree();

            switch (tipo)
            {
                case "Free":
                    calculo = new CalculoFree();
                    break;
                case "Standard":
                    calculo = new CalculoStandard();
                    break;
                case "Corporate":
                    calculo = new CalculoCorporate();
                    break;

            }

            var contaAgregate = new ContaAgregate(new IdConta(ObjectId.GenerateNewId().ToString()), calculo);
            contaAgregate.Tipo = new(tipo);
            contaAgregate.IdCustomer = idCustomer;
            contaAgregate.QtdColaboradores = 3;
            contaAgregate.DataCriacao = DateTime.Now;
            contaAgregate.Status = new StatusConta("Iniciado");

            contaAgregate.DTO = new()
            {
                Tipo = contaAgregate.Tipo.Texto,
                Id = contaAgregate.Id.MongoGuid,
                IdCustomer = contaAgregate.IdCustomer,
                DataCriacao = contaAgregate.DataCriacao,
                Status = "Iniciado"
            };

           await unitOfWork.RepoConta.Salvar(contaAgregate.DTO);

            return contaAgregate;
        }
    }
}
