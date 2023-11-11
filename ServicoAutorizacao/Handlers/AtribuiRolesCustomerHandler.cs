using EDSCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ServicoAutorizacao;

namespace ServiceCustomer.Handlers
{

    public class RoleCustomerCommand
    {
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    public class AtribuiRolesCustomerCommand : IRequest<Resultado<IdentityResult, ValidationFalhas>>
    {
        public AtribuiRolesCustomerCommand(List<RoleCustomerCommand> roles)
        {
            Roles = roles;
        }

        public List<RoleCustomerCommand> Roles { get; set; }
    }


    public class AtribuiRolesCustomerHandler : IRequestHandler<AtribuiRolesCustomerCommand, Resultado<IdentityResult, ValidationFalhas>>
    {
        private UserManager<ApplicationUser> _userManager;
        public AtribuiRolesCustomerHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Resultado<IdentityResult, ValidationFalhas>> Handle(AtribuiRolesCustomerCommand request, CancellationToken cancellationToken)
        {
            var erros = new List<ValidationFalha>();
            try
            {
                IdentityResult result = null;
                string email = request.Roles[0].Email;
                var user = await _userManager.FindByEmailAsync(request.Roles[0].Email);
                foreach (var role in request.Roles)
                {
                    if (email != role.Email)
                    {
                        user = await _userManager.FindByEmailAsync(role.Email);
                    }

                    result = await _userManager.AddToRoleAsync(user, role.Role);
                    if (!result.Succeeded)
                    {
                        return result;
                    }
                }

                return result;
            }
            catch (Exception)
            {

                //erros.Add(new ValidationFalhas("Exception", ex.Message));
                //return new ValidationFalha(erros.ToArray());
                throw;
            }
        }
    }
}
