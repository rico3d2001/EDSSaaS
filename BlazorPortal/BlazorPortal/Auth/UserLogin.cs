using System.ComponentModel.DataAnnotations;

namespace BlazorPortal.Auth
{
    public class UserLogin
    {
        const string EMAIL_TOKEN_ATIVO = "89836650";

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string EmailTokenAtivo { get; set; } = string.Empty;
    }
}
