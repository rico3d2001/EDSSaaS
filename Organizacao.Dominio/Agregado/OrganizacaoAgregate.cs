using EDSCore;
using MongoDB.Bson;
using Organizacao.Dominio.Command;
using Organizacao.Dominio.Interfaces;
using Organizacao.Dominio.ValueObjects;
using OrganizacaoDTOS;

namespace Organizacao.Dominio.Agregado
{
    public class OrganizacaoAgregate : Entidade<IdOrganizacao>, IAggregateRoot
    {
        private OrganizacaoAgregate(IdOrganizacao idOrganizacao) :base(idOrganizacao)
        {      
        }
        public OrganizacaoDOC DTO { get; set; }
        
       
        public static async Task<OrganizacaoAgregate> CriarOrganizacao(IniciaOrganizacaoCommand command, IUnitOfWorkOrganizacao unitOfWork)
        {
            try
            {

                var organizacaoAgregate = new OrganizacaoAgregate(new IdOrganizacao(ObjectId.GenerateNewId().ToString()));
                organizacaoAgregate.DTO = new OrganizacaoDOC()
                {
                    Id = organizacaoAgregate.Id.MongoGuid,
                    IdConta = command.IdConta,
                    Nome = command.Nome,
                    LogoMarca = command.Logomarca,
                    CNPJ = command.CNPJ
                };

                await unitOfWork.RepoOrganizacao.Salvar(organizacaoAgregate.DTO);

                return organizacaoAgregate;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
