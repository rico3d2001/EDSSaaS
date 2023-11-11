using APIGatewayEDS.Commands;
using ContratoDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIGatewayEDS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/v1")]
    public class ContratoController : EDSController
    {

        public ContratoController(IMediator mediator, HttpClient httpClient
            ) : base(mediator, httpClient)
        {
            
        }

        //[AllowAnonymous]
        [HttpGet("ObterContratos/{idOrganizacao}")]
        public async Task<IActionResult> ObterContratos(string idOrganizacao)
        {
            try
            {
                return await LinkarGet(idOrganizacao);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

        

        [AllowAnonymous]
        [HttpPost("IniciarContrato")]
        public async Task<IActionResult> IniciarContrato(IniciarContratoCommand command)
        {

            try
            {
                var uri = $"http://apicontratos/api/Contrato/v1/IniciarContrato/";
                return await Linkar(_httpClient.PostAsJsonAsync(uri, command).Result, uri);

                //var httpResponse = _httpClient.PostAsJsonAsync(uri, command).Result;

                //if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                //{
                //    return BadRequest();
                //}

                //if (httpResponse.Content is object && httpResponse.Content.Headers.ContentType.MediaType == "application/json")
                //{
                //    var contentStream = await httpResponse.Content.ReadAsStreamAsync();
                //    using var streamReader = new StreamReader(contentStream);
                //    using var jsonReader = new JsonTextReader(streamReader);
                //    JsonSerializer serializer = new JsonSerializer();
                //    try
                //    {
                //        var objeto = serializer.Deserialize<ContratoDOC>(jsonReader);
                //        return Ok(objeto);
                //    }
                //    catch (JsonReaderException)
                //    {
                //        return BadRequest("Invalid JSON.");

                //    }
                //}

                //return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }


    }
}
