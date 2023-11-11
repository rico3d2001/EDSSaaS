namespace APIGatewayEDS.Commands
{
    public class IniciarContratoCommand
    {
        public string IdOrganizacao { get; set; }
        public string NumeroContrato { get; set; }
        public string NumeroCliente { get; set; }
        public string Descricao { get; set; }
        public string TipoContratacao { get; set; }
        public string FormaMedicao { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataContrato { get; set; }
        public DateTime PrazoVigencia { get; set; }
        public DateTime PrazoEscopo { get; set; }
        public DateTime PeriodoMedicaoInicio { get; set; }
        public DateTime PeriodoMedicaoFim { get; set; }
        public int PrazoComentariosDias { get; set; }

    }
}



           
            