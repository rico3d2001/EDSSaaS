using FluentValidation;
using Hubs.Dominio.Commands;

namespace ServiceHub.Validacoes
{
    public class ConfirmaCustomerValidator : AbstractValidator<ConfirmaCustomerCommand> 
    {
        public ConfirmaCustomerValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Id é um campo de preenchimento obrigatório");
            RuleFor(x => x.CPF).NotEmpty().IsValidCPF().WithMessage("Use um número de CPF válido");
            RuleFor(x => x.Foto).NotEmpty().NotEqual("/do-utilizador.png").WithMessage("Icone deve ter sido substituido por foto");
        }
    }
}

