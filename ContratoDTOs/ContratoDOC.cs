using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContratoDTOs
{
    public class ContratoDOC
    {
        [BsonElement("Id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string IdOrganizacao { get; set; } = string.Empty;
        public string NumeroContrato { get; set; } = string.Empty;
        public string NumeroCliente { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string TipoContratacao { get; set; } = string.Empty;
        public string FormaMedicao { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public DateTime DataContrato { get; set; }
        public DateTime PrazoVigencia { get; set; }
        public DateTime PrazoEscopo { get; set; }
        public DateTime PeriodoMedicaoInicio { get; set; }
        public DateTime PeriodoMedicaoFim { get; set; }
        public int PrazoComentariosDias { get; set; }
        //public List<ProjetosDOC> Projetos { get; set; } = new List<ProjetosDOC>();
        //public ClienteDOC Cliente { get; set; } = new ClienteDOC();
        //public List<ClaimDOC> AtributosEspecificos { get; set; } = new List<ClaimDOC>();

    }
}