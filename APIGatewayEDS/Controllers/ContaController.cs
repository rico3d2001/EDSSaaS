using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGatewayEDS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/v1")]
    public class ContaController : EDSController
    {
        
        public ContaController(IMediator mediator, HttpClient httpClient) : base(mediator,httpClient)
        {
        }

        //[AllowAnonymous]
        [HttpGet("ChecarStatus/{idCustomer}")]
        public async Task<IActionResult> ChecarStatus(string idCustomer)
        {
            try
            {
                var uri = $"http://webapicontas/api/Conta/v1/ChecarStatus/{idCustomer}";
                return await Linkar(uri);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        

        //[AllowAnonymous]
        [HttpPut("IniciarConta")]
        public async Task<IActionResult> InciarConta(IniciaContaRequest command)
        {
            try
            {
                var uri = $"http://webapicontas/api/Conta/v1/IniciarConta/";
                return await Linkar(_httpClient.PutAsJsonAsync(uri, command).Result, uri);
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
