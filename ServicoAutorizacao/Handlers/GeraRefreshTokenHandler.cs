using EDSCore;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using ServicoAutorizacao;
using ServicoAutorizacao.Configs;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace ServiceCustomer.Handlers
{
    public class GeraRefreshTokenCommand : IRequest<Resultado<TokenResponse, ValidationFalhas>>
    {
        public GeraRefreshTokenCommand(string token, string refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }

        public string Token { get; set; }
        public string RefreshToken { get; set; }

    }

    public class GeraRefreshTokenHandler : IRequestHandler<GeraRefreshTokenCommand, Resultado<TokenResponse, ValidationFalhas>>
    {
        private readonly IMongoCollection<DocRefreshToken> _refreshCollection;
        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshHandler _refresh;
        public GeraRefreshTokenHandler(IOptions<HubDbConfig> authDatabaseSettings, IOptions<JwtSettings> jwtSettings, IRefreshHandler refresh)
        {
            var mongoClient = new MongoClient(
                authDatabaseSettings.Value.Connection);

            var mongoDatabase = mongoClient.GetDatabase(
                authDatabaseSettings.Value.DatabaseName);

            _refreshCollection = mongoDatabase.GetCollection<DocRefreshToken>(
                authDatabaseSettings.Value.RefreshTokensCollectionName);

            _jwtSettings = jwtSettings.Value;
            _refresh = refresh;

        }


        public async Task<Resultado<TokenResponse, ValidationFalhas>> Handle(GeraRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var erros = new List<ValidationFalha>();
            var refreshtoken = await _refreshCollection.Find(x => x.RefreshToken == request.RefreshToken).FirstOrDefaultAsync();
            if (refreshtoken != null)
            {
                //generate token
                var tokenhandler = new JwtSecurityTokenHandler();
                var tokenkey = Encoding.UTF8.GetBytes(_jwtSettings.securitykey);
                SecurityToken securityToken;
                var principal = tokenhandler.ValidateToken(request.Token, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(tokenkey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out securityToken);

                var _token = securityToken as JwtSecurityToken;
                if (_token != null && _token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                {
                    string username = principal.Identity?.Name;
                    var existData = await _refreshCollection
                        .Find(x => x.UserId == username && x.RefreshToken == request.RefreshToken)
                        .FirstOrDefaultAsync();
                    if (existData != null)
                    {
                        var newtoken = new JwtSecurityToken(
                              claims: principal.Claims.ToArray(),
                              expires: DateTime.Now.AddSeconds(30),
                              signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.securitykey)),
                              SecurityAlgorithms.HmacSha256)
                            );

                        var _finaltoken = tokenhandler.WriteToken(newtoken);
                        return new TokenResponse()
                        {
                            Token = _finaltoken,
                            RefreshToken = await _refresh.GenerateToken(username),
                        };
                    }
                    else
                    {
                        erros.Add(new ValidationFalha("refreshToken", "Não autorizado a renovar token."));
                        return new ValidationFalhas(erros.ToArray());
                    }
                }
                else
                {
                    erros.Add(new ValidationFalha("refreshToken", "Não autorizado a renovar token."));
                    return new ValidationFalhas(erros.ToArray());
                }
             
            }
            else
            {
                erros.Add(new ValidationFalha("refreshToken", "Não autorizado a renovar token."));
                return new ValidationFalhas(erros.ToArray());
            }
        }
    }
}
