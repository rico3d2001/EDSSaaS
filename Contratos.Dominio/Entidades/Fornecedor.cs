using Contratos.Dominio.ValueObjects;
using Contratos.Dominio.ValueObjects.IdsValue;
using EDSCore;

namespace Contratos.Dominio.Entidades
{
    public class Fornecedor : Entidade<IdFornecedor>
    {
        public Fornecedor(IdFornecedor id) : base(id)
        {
        }

        public SiglaProfissional Sigla { get; set; }
        public string Nome { get; set; }
        public string NomeCompleto { get; set; }
        public Email Email { get; set; }
        public IdContrato IdContrato { get; set;}
    }
}
