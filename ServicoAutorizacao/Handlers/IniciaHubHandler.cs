using EDSCore;
using HubDTOs.Documentos;
using Hubs.Dominio.Agregados;
using Hubs.Dominio.Commands;
using Hubs.Dominio.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ServicosEmailHub;
using System.Security.Claims;

namespace ServicoAutorizacao.Handlers
{


    public class IniciaHubHandler : IRequestHandler<IniciaHubCommand, Resultado<HubDOC, ValidationFalhas>>
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWorkHub _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly HttpClient _httpClient;
        public IniciaHubHandler(IMediator mediator, IUnitOfWorkHub unitOfWork, 
            UserManager<ApplicationUser> userManager, IEmailSender emailSender, HttpClient httpClient)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailSender = emailSender;
            _httpClient = httpClient;
        }

        public async Task<Resultado<HubDOC, ValidationFalhas>> Handle(IniciaHubCommand command, CancellationToken cancellationToken)
        {
            var erros = new List<ValidationFalha>();

            var codigoEmail = new Random().Next(99999999).ToString();

            try
            {
                
                string nomeUser = $"{command.UserName}-{Guid.NewGuid()}";

                ApplicationUser appUser = new()
                {
                    UserName = nomeUser,
                    Email = command.Email
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, command.Password);

                await _userManager.AddToRoleAsync(appUser, "indefinido");

                var claimCriado = new Claim("emailtokenativo", codigoEmail);
                result = await _userManager.AddClaimAsync(appUser, claimCriado);

                var mensagem = new MensagemEmail(
                          from: "rico3d2001@gmail.com",
                          to: command.Email,
                          displayname: "Rico3d Projetos e Soluções Ltda",
                          subject: "Número de confirmação de e-mail para atutenticação",
                          content: $"Digite este número: {codigoEmail} no login da Plataforma EDS Engineering Designs System");

                _emailSender.SendEmail(mensagem);


                var hubAgregate = await HubAgregate.IniciarHub(command, _unitOfWork);

                return hubAgregate.DTO;


            }
            catch (Exception ex)
            {
                erros.Add(new ValidationFalha("500", ex.Message));
                return new ValidationFalhas(erros);
            }

        }

        

    }

}
