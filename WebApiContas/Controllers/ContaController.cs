using Conta.Dominio.Commands;
using Conta.Dominio.Interfaces;
using ContasAPI.Configs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ContasAPI.Controllers
{
    //[Authorize]
    //[DisableCors]
    // [EnableRateLimiting("fixedwindow")]
    [ApiController]
    [Route("api/[controller]/v1")]
    public class ContaController : EDSController
    {
        private readonly string _aWSAcessKeyId;
        private readonly string _aWSSecretKeyId;
        private readonly IWebHostEnvironment _environment;
        private readonly string _domainName;
        private readonly string _bucketName;
        private readonly IUnitOfWorkConta _serviceQuery;


        public ContaController(IMediator mediator,
            IOptions<S3ImageCronosConfig> s3ImageCronosConfig, 
            IWebHostEnvironment environment, IUnitOfWorkConta serviceQuery) : base(mediator)
        {
            _serviceQuery = serviceQuery;
             _environment = environment;
            _aWSAcessKeyId = s3ImageCronosConfig.Value.AWSAcessKeyId;
            _aWSSecretKeyId = s3ImageCronosConfig.Value.AWSSecretKeyId;
            _domainName = s3ImageCronosConfig.Value.DomainName;
            _bucketName = s3ImageCronosConfig.Value.BucketName;
        }


        //[AllowAnonymous]
        [HttpGet("ChecarStatus/{idCustomer}")]
        public async Task<IActionResult> ChecarStatus(string idCustomer)
        {

            try
            {
                var command = new ChecarStatusCommand(idCustomer);
                var result = await _mediator.Send(command);
                var entender = result.Match<IActionResult>(
                    m => CreatedAtAction(nameof(InciarConta), new { id = m.Id }, m),
                    failed => BadRequest(failed.Errors));
                return entender;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            //try
            //{
            //    var data = await _serviceQuery.RepoConta.ObterUltimaContaPorCustomer(idCustomer);
            //    if (data == null)
            //    {
            //        return NotFound($"Não encontrado conta correspondente ao cliente");
            //    }
            //    return Ok(data);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}

        }

        //[AllowAnonymous]
        [HttpPut("IniciarConta")]
        public async Task<IActionResult> InciarConta(IniciaContaRequest request)
        {
            try
            {
                var command = new IniciaContaCommand(request.IdCustomer,request.Tipo);
                var result = await _mediator.Send(command);
                var entender = result.Match<IActionResult>(
                    m => CreatedAtAction(nameof(InciarConta), new { id = m.Id }, m),
                    failed => BadRequest(failed.Errors));
                return entender;

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }

 
    public class IniciaContaRequest
    {
        public string IdCustomer { get; set; }
        public string Tipo { get; set; }
    }

   

   


}


