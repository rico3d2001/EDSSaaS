using Contratos.Dominio.Commands;
using Contratos.Dominio.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValidacaoHelper.Notification;

namespace APIContratos.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class ContratoController : EDSController
    {
        private readonly IDomainNotificationContext _notificationContext;
        private readonly IUnitOfWorkContratos _serviceQuery;
        public ContratoController(IMediator mediator, IDomainNotificationContext notificationContext, IUnitOfWorkContratos serviceQuery) : base(mediator)
        {
            _notificationContext = notificationContext;
            _serviceQuery = serviceQuery;
        }

        [HttpPost("IniciarContrato")]
        public async Task<IActionResult> IniciarContrato(IniciarContratoCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if (_notificationContext.HasErrorNotifications)
                {
                    var notifications = _notificationContext.GetErrorNotifications();
                    var message = string.Join(", ", notifications.Select(x => x.Value));
                    return BadRequest(message);
                }


                var entender = result.Match<IActionResult>(
                    m => CreatedAtAction(nameof(IniciarContrato), new { id = m.Id }, m),
                    failed => BadRequest(failed.Errors));
                return entender;

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{idOrganizacao}")]
        public async Task<IActionResult> ObterContratos(string idOrganizacao)
        {
            try
            {
                var data = await _serviceQuery.RepoContratos.ObterContratos(idOrganizacao);
                if (data == null)
                {
                    return NotFound($"Não encontrado contrato correspondente ao cliente");
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}