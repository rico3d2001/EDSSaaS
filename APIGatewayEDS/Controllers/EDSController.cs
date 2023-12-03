using ContaDTOs;
using ContratoDTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace APIGatewayEDS.Controllers
{
    public class EDSController : ControllerBase
    {
        protected IMediator _mediator;
        protected readonly HttpClient _httpClient;
        public EDSController(IMediator mediator, HttpClient httpClient)
        {
            _mediator = mediator;
            _httpClient = httpClient;
        }

        protected async Task<IActionResult> Linkar(HttpResponseMessage httpResponse, string uri)
        {
            //var httpResponse = _httpClient.PutAsJsonAsync(uri, command).Result;

            if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest();
            }

            if (httpResponse.Content is object && httpResponse.Content.Headers.ContentType.MediaType == "application/json")
            {
                var contentStream = await httpResponse.Content.ReadAsStreamAsync();
                using var streamReader = new StreamReader(contentStream);
                using var jsonReader = new JsonTextReader(streamReader);
                JsonSerializer serializer = new JsonSerializer();
                try
                {
                    var objeto = serializer.Deserialize<ContaDOC>(jsonReader);
                    return Ok(objeto);
                }
                catch (JsonReaderException)
                {
                    return BadRequest("Invalid JSON.");

                }
            }

            return BadRequest();
        }

        protected async Task<IActionResult> Linkar(string uri)
        {
            var httpResponse = await _httpClient.GetAsync(uri);

            if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest();
            }

            if (httpResponse.Content is object && httpResponse.Content.Headers.ContentType.MediaType == "application/json")
            {
                var contentStream = await httpResponse.Content.ReadAsStreamAsync();
                using var streamReader = new StreamReader(contentStream);
                using var jsonReader = new JsonTextReader(streamReader);
                JsonSerializer serializer = new JsonSerializer();
                try
                {
                    var objeto = serializer.Deserialize<ContaDOC>(jsonReader);
                    return Ok(objeto);
                }
                catch (JsonReaderException)
                {
                    return BadRequest("Invalid JSON.");

                }
            }

            return BadRequest();
        }

        protected async Task<IActionResult> LinkarGet(string idOrganizacao)
        {
            
            //var uri = $"https://localhost:32775/apicontratos/api/Contrato/v1/{idOrganizacao}";
            var uri = $"http://apicontratos/api/Contrato/v1/{idOrganizacao}";

            var responseString = await _httpClient.GetStringAsync(uri);

            var catalog = JsonConvert.DeserializeObject<List<ContratoDOC>>(responseString);
            return Ok(catalog);
        }
    }
}
