namespace ServicoAutorizacao.Configs
{
    public class HubDbConfig
    {
        public string Connection { get; set; }
        public string DatabaseName { get; set; }
        //public string CustomerCollectionName { get; set; }
        //public string RoleAccessCollectionName { get; set; }
        //public string AuthSimplesCollectionName { get; set; }
        public string RefreshTokensCollectionName { get; set; }

    }
}
