using FluentValidation;
using Organizacao.Dominio.Command;

namespace ServiceOrganizacoes.Validacoes
{
    public class CriarOrganizacaoValidator : AbstractValidator<IniciaOrganizacaoCommand>
    {
        public CriarOrganizacaoValidator()
        {
            RuleFor(x => x.IdConta).NotEmpty().WithMessage("IdConta não pode ser vazio");
            RuleFor(x => x.Nome).NotEmpty().MinimumLength(3).WithMessage("Comprimento do nome deve ter no mínimo 3 caracteres");
            RuleFor(x => x.Logomarca).NotEmpty().MinimumLength(3).WithMessage("Comprimento do nome deve ter no mínimo 3 caracteres");
            RuleFor(x => x.CNPJ).NotEmpty().Length(18).WithMessage("Comprimento do CNPJ é de 18 caracteres");
        }
    }
}

