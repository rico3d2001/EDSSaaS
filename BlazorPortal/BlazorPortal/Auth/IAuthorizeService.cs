namespace BlazorPortal.Auth
{
    public interface IAuthorizeService
    {
        Task Login(string username, string password);
        Task Logout();
        Task<bool> Register(string username, string password);
    }
}
