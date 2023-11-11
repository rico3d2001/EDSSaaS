using EDSCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ServicoAutorizacao.Handlers
{
    public class ClaimCommand
    {
        public string Email { get; set; } = string.Empty;
        public string Chave { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
    }
    public class AtribuiClaimsCommand : IRequest<Resultado<IdentityResult, ValidationFalhas>>
    {
        public AtribuiClaimsCommand(List<ClaimCommand> claims)
        {
            Claims = claims;
        }

        public List<ClaimCommand> Claims { get; set; }

    }
    public class AtribuiClaimsHandler : IRequestHandler<AtribuiClaimsCommand, Resultado<IdentityResult, ValidationFalhas>>
    {
        private UserManager<ApplicationUser> _userManager;
        public AtribuiClaimsHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Resultado<IdentityResult, ValidationFalhas>> Handle(AtribuiClaimsCommand request, CancellationToken cancellationToken)
        {
            var erros = new List<ValidationFalha>();
            try
            {
                IdentityResult result = null;
                string email = request.Claims[0].Email;
                var user = await _userManager.FindByEmailAsync(email);
                foreach (var claimModal in request.Claims)
                {
                    if (email != claimModal.Email)
                    {
                        user = await _userManager.FindByEmailAsync(claimModal.Email);
                    }
                    var claimCriado = new Claim(claimModal.Chave, claimModal.Valor);
                    result = await _userManager.AddClaimAsync(user, claimCriado);
                    if (!result.Succeeded)
                    {
                        return result;
                    }
                }

                return result;
            }
            catch (Exception)
            {

                //erros.Add(new ValidationFalha("Exception", ex.Message));
                //return new ValidationFalhas(erros.ToArray());
                throw;
            }
            
        }
    }
}
