using EDSCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServicoAutorizacao;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceCustomer.Handlers
{

    public class GeraTokenCommand : IRequest<Resultado<TokenResponse, ValidationFalhas>>
    {
        
        public GeraTokenCommand(string email, string password, string numeroEmail)
        {
            Email = email;
            Password = password;
            NumeroEmail = numeroEmail;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string NumeroEmail { get; set; }


    }


    public class GeraTokenHandler : IRequestHandler<GeraTokenCommand, Resultado<TokenResponse, ValidationFalhas>>
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshHandler _refresh;

        public GeraTokenHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IOptions<JwtSettings> jwtSettings, IRefreshHandler refresh)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _refresh = refresh;
        }

        public async Task<Resultado<TokenResponse, ValidationFalhas>> Handle(GeraTokenCommand request, CancellationToken cancellationToken)
        {
            var erros = new List<ValidationFalha>();
            ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);

            if (!user.EmailConfirmed)
            {
                var emailtokenativo = user.Claims.FirstOrDefault(x => x.Type == "emailtokenativo" && x.Value == request.NumeroEmail);
                if (emailtokenativo == null)
                {
                    erros.Add(new ValidationFalha("emailtokenativo", "O número digitado não corresponde ao do email enviado."));
                    return new ValidationFalhas(erros.ToArray());
                }

                user.EmailConfirmed = true;
                _userManager.UpdateAsync(user);

            }


            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (result.Succeeded)
            {
                //generate token
                var tokenhandler = new JwtSecurityTokenHandler();
                var tokenkey = Encoding.UTF8.GetBytes(_jwtSettings.securitykey);
                var tokendesc = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.Role, "indefinido"),
                    }),
                    //Expires = DateTime.UtcNow.AddSeconds(30),
                    Expires = DateTime.UtcNow.AddSeconds(432000),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
                };
                var token = tokenhandler.CreateToken(tokendesc);
                var finaltoken = tokenhandler.WriteToken(token);
                return new TokenResponse()
                {
                    Token = finaltoken,
                    RefreshToken = await _refresh.GenerateToken(request.Email),
                };
            }

            erros.Add(new ValidationFalha("token", "Email, senha ou indentificador do email inadequados"));
           return new ValidationFalhas(erros.ToArray());
        }


    }
}
