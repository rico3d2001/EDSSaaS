using EDSCore;
using HubDTOs.Documentos;
using Hubs.Dominio.Commands;
using Hubs.Dominio.Entities;
using Hubs.Dominio.Interfaces;
using MongoDB.Bson;


namespace Hubs.Dominio.Agregados
{
    public record IdHub
    {
        private IdHub()
        {

        }
        public IdHub(string id)
        {
            MongoGuid = id;
        }

        public string MongoGuid { get; private set; }
    }
    public class HubAgregate : Entidade<IdHub>, IAggregateRoot
    {
        private HubAgregate(IdHub id) : base(id)
        {
        }

        public HubDOC DTO { get; set; }

        
        public static async Task<HubAgregate> IniciarHub(IniciaHubCommand command, IUnitOfWorkHub unitOfWork)
        {
            var hubAgregate = new HubAgregate(new IdHub(ObjectId.GenerateNewId().ToString()));

            Customer customer = new(new IdCustomer(hubAgregate.Id.MongoGuid));

            hubAgregate.DTO = new HubDOC()
            {
                Id = hubAgregate.Id.MongoGuid,
                Email = command.Email,
                Customers = new List<CustomerDOC> {
                        new CustomerDOC
                        {
                            Id = customer.Id.MongoGuid,
                            Name = command.UserName,
                            Phone = "",
                            CPF = "",
                            Foto = "/do-utilizador.png",
                            Status = "Iniciado",
                            Email = command.Email
                        }
                    }
            };

            await unitOfWork.HubRepositorio.Iniciar(hubAgregate.DTO);

            return hubAgregate;
        }

        public static async Task<HubAgregate> ConfirmaCustomer(ConfirmaCustomerCommand command, IUnitOfWorkHub unitOfWork)
        {
            var dto = await unitOfWork.HubRepositorio.GetById(command.Id);

            if (dto == null)
            {
                throw new Exception("Hub ainda não foi cadastrado");
            }

            var customerPrincipal = dto.Customers.FirstOrDefault();
            if (customerPrincipal == null)
            {
                throw new Exception("Customer principal ainda não existe");
            }

            customerPrincipal.CPF = command.CPF;
            customerPrincipal.Phone = command.Phone;
            customerPrincipal.Foto = command.Foto;

            var hubAgregate = new HubAgregate(new IdHub(dto.Id));

            hubAgregate.DTO = dto;

            await unitOfWork.HubRepositorio.Confirmar(dto);

            return hubAgregate;
        }

    }
}


