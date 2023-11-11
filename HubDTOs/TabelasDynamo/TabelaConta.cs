namespace HubDTOs.TabelasDynamo
{
    public class TabelaConta
    {
        public string IdConta { get; set; }
        public string IdCustomer { get; set; }
        public string EmailCustomer { get; set; }
        public string Tipo { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Status { get; set; }
        public string JustificativaStatus { get; set; }
    }
}
