using Amazon.S3;
using Amazon.S3.Transfer;
using APIGatewayEDS.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrganizacaoDTOS;
using ServiceFotoUsuario;

namespace APIGatewayEDS.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]/v1")]
    public class OrganizacaoController : EDSController
    {
        private readonly string _aWSAcessKeyId;
        private readonly string _aWSSecretKeyId; 
        private readonly string _domainName;
        private readonly string _bucketName;
        public OrganizacaoController(IMediator mediator, IOptions<S3ImageCronosConfig> s3ImageCronosConfig, HttpClient httpClient) : base(mediator, httpClient)
        {
            _aWSAcessKeyId = s3ImageCronosConfig.Value.AWSAcessKeyId;
            _aWSSecretKeyId = s3ImageCronosConfig.Value.AWSSecretKeyId;
            _domainName = s3ImageCronosConfig.Value.DomainName;
            _bucketName = s3ImageCronosConfig.Value.BucketName;
        }

        [HttpPut("UploadLogomarca/{nome}")]
        [DisableRequestSizeLimit]
        public async Task<Ok<string>> UploadLogoMarca(List<IFormFile> files, string nome)//Task<Ok<string>> UploadFotoUsuario(List<IFormFile> files, string email)
        {
            //Para varios é IFormFileCollection com o recurso do TransferUtility de multiplos

            try
            {
                using (var client = new AmazonS3Client(_aWSAcessKeyId, _aWSSecretKeyId, Amazon.RegionEndpoint.USEast1))
                {
                    using (var newMemoryStream = new MemoryStream())
                    {
                        files[0].CopyTo(newMemoryStream);

                        var uploadRequest = new TransferUtilityUploadRequest
                        {
                            InputStream = newMemoryStream,
                            Key = nome + ".png",
                            BucketName = _bucketName,
                            CannedACL = S3CannedACL.PublicRead
                        };

                        var fileTransferUtility = new TransferUtility(client);
                        await fileTransferUtility.UploadAsync(uploadRequest);
                    }
                }

                var resp = _domainName + "/" + nome + ".png";
                return TypedResults.Ok(resp);

            }
            catch (Exception ex)
            {
                return null;
            }

            //Para varios é IFormFileCollection com o recurso do TransferUtility de multiplos


        }

        [HttpGet("Conta/{idConta}")]
        public async Task<IActionResult> ObterOrganizacoesCliente(string idConta)
        {
            try
            {
                //var uri = $"https://wfusubd9qb.us-east-1.awsapprunner.com/api/Organizacao/v1/Conta/{idConta}";
                //var uri = $"https://localhost:7278/api/Organizacao/v1/Conta/{idConta}";
                var uri = $"http://webapiorganizacao/api/Organizacao/v1/Conta/{idConta}";

                var responseString = await _httpClient.GetStringAsync(uri);

                var catalog = JsonConvert.DeserializeObject<List<OrganizacaoDOC>>(responseString);
                return Ok(catalog);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CriarOrganizacao")]
        public async Task<IActionResult> CriarOrganizacao(IniciaOrgCommand command)
        {
            try
            {
                //var uri = $"https://wfusubd9qb.us-east-1.awsapprunner.com/api/Organizacao/v1/CriarOrganizacao/";
                //var uri = $"https://localhost:7278/api/Organizacao/v1/CriarOrganizacao/";
                var uri = $"http://webapiorganizacao/api/Organizacao/v1/CriarOrganizacao/";

                var httpResponse = _httpClient.PostAsJsonAsync(uri, command).Result;

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
                        var objeto = serializer.Deserialize<OrganizacaoDOC>(jsonReader);
                        return Ok(objeto);
                    }
                    catch (JsonReaderException)
                    {
                        return BadRequest("Invalid JSON.");

                    }
                }

                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
