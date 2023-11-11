using ContratoDTOs;
using Contratos.Dominio.Commands;
using EDSCore;
using FluentValidation;
using MediatR;
using System.Text.RegularExpressions;

namespace ServiceContratos.Validacoes
{
    public class InciaContratoValidator : AbstractValidator<IniciarContratoCommand>
    {

        public InciaContratoValidator()
        {
            string ano = DateTime.Now.Year.ToString().Substring(2,2);

            string formarRegexCliente = @"^[A-Z]{0,10}_[0-9]{4}-" + ano + "$"; 
            Regex rxNumeroCliente = new Regex(formarRegexCliente,
                    RegexOptions.Compiled | RegexOptions.IgnoreCase);

            string formarRegexEDS = @"^(EDS)_[0-9]{4}-" + ano + "$";
            Regex rxnumeroContrato = new Regex(formarRegexEDS,
                RegexOptions.Compiled | RegexOptions.IgnoreCase);


            RuleFor(x => x.IdOrganizacao).NotEmpty().WithMessage("IdConta não pode ser vazio");
            RuleFor(x => x.NumeroCliente).Must(m => rxNumeroCliente.IsMatch(m))
                     .WithMessage("O numero do cliente não está no formato correto");
            RuleFor(x => x.NumeroContrato).Must(m => rxNumeroCliente.IsMatch(m))
                     .WithMessage("O numero EDS não está no formato correto");
            RuleFor(x => x.Descricao).NotEmpty().WithMessage("Preenchimento da Descrição é obrigatória");

        }
    }
}

