namespace ServicoAutorizacao
{
    public interface IRefreshHandler
    {
        Task<string> GenerateToken(string username);
    }
}
