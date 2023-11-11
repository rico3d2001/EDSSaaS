using FluentValidation;
using Hubs.Dominio.Commands;

namespace ServicoAutorizacao.Validacoes
{
    public class IniciaHubValidator : AbstractValidator<IniciaHubCommand>
    {
        public IniciaHubValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Use um endreço de email válido");
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(3).WithMessage("Comprimento do nome deve ter no mínimo 3 caracteres");
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(15).WithMessage("Comprimento do nome deve ter no máximo 15 caracteres");
        }
    }
}


