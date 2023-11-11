using ContratoDTOs;
using Contratos.Dominio.Commands;
using Contratos.Dominio.Enumeradores;
using Contratos.Dominio.Interfaces;
using Contratos.Dominio.ValueObjects;
using Contratos.Dominio.ValueObjects.IdsValue;
using Contratos.Dominio.ValueObjects.Moedas;
using EDSCore;
using MongoDB.Bson;

namespace Contratos.Dominio.Agregados
{
    public class ContratoAgregate : Entidade<IdContrato>, IAggregateRoot
    {
        List<CriterioProporcaoMedicao> _criteriosMedicao;
        VersaoContrato _versaoContrato;

        private ContratoAgregate(IdContrato id) : base(id)
        {
            _criteriosMedicao = new List<CriterioProporcaoMedicao>();
        }

        public ContratoDOC DTO { get; set; }

        public UnidadeDeValor ValorUnitario { get; set; }
        public int UnidadesContratadas { get; set; }

        public PeriodoMedicao PeriodoMedicao { get; set; }
        public PrazoEmDias PrazoAtendimentoComentarios { get; set; }
        public PrazoEmDias PrazoPagamentoPosMedicao { get; set; }
        public DateTime DataEmissaoNotaFiscal { get; set; }
        public DateTime VigenciaContrato { get; set; }
        public DateTime DataContrato { get; set; }

        public static async Task<ContratoAgregate> IniciarContrato(IniciarContratoCommand command, IUnitOfWorkContratos unitOfWork)
        {
            try
            {
                var contrato = new ContratoAgregate(new IdContrato(ObjectId.GenerateNewId().ToString()));
                contrato.DTO = new ContratoDOC()
                {
                    Id = contrato.Id.MongoGuid,
                    IdOrganizacao = command.IdOrganizacao,
                    NumeroCliente = command.NumeroCliente,
                    NumeroContrato = command.NumeroContrato,
                    Descricao = command.Descricao,
                    TipoContratacao = command.TipoContratacao,
                    FormaMedicao = command.FormaMedicao,
                    Quantidade = command.Quantidade,
                    DataContrato = command.DataContrato,
                    PrazoVigencia = command.PrazoVigencia,
                    PrazoEscopo = command.PrazoEscopo,
                    PeriodoMedicaoInicio = command.PeriodoMedicaoInicio,
                    PeriodoMedicaoFim = command.PeriodoMedicaoFim,
                    PrazoComentariosDias = command.PrazoComentariosDias
                };

                await unitOfWork.RepoContratos.Salvar(contrato.DTO);

                return contrato;
            }
            catch (Exception)
            {

                throw;
            }



        }

        public void InserirCriterioMedicao(decimal percentual, ValorEmReais valor)
        {
            if (percentual > 100)
            {
                throw new Exception("Percentual não pode exceder 100%");
            }

            if (_criteriosMedicao.Count == 0)
            {
                _criteriosMedicao.Add(new CriterioProporcaoMedicao(0, percentual, valor));
                return;
            }

            var percentualTotal = _criteriosMedicao.Sum(x => x.Percentual);

            if (percentualTotal > 100)
            {
                throw new Exception("Percentual somado não pode exceder 100%");
            }

            var indiceCalculado = _criteriosMedicao.Max(x => x.Indice) + 1;
            _criteriosMedicao
                .Add(new CriterioProporcaoMedicao(indiceCalculado, percentual, valor));

        }

        public ValorEmReais ValorTotal
        {
            get
            {
                if (ValorUnitario.Tipo == TipoContratacao.A1eq)
                {
                    var valorTotal = UnidadesContratadas * ValorUnitario.Valor.Valor;
                    var moeda = ValorUnitario.Valor.Moeda;
                    return new ValorEmReais(valorTotal);
                }

                return null;
            }
        }

        public VersaoContrato VersaoContrato { get => _versaoContrato; }

        public void VersionaContrato()
        {
            if (_versaoContrato == null)
            {
                _versaoContrato = new VersaoContrato(0, DateTime.Now);
                return;
            }

            _versaoContrato = new VersaoContrato(_versaoContrato.Indice + 1, DateTime.Now);


        }
    }
}
