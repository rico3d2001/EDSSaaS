namespace ServicoAutorizacao.Configs
{
    public class MongoDbConfig
    {
        public string Name { get; set; }
        public string Usuario { get; init; }
        public string Senha { get; init; }
        public string ConnectionString => $"mongodb+srv://{Usuario}:{Senha}@cluster0.fnm6cxr.mongodb.net/?retryWrites=true&w=majority";
    }
}
