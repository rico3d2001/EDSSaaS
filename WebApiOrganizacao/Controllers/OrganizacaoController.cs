using Amazon.S3;
using Amazon.S3.Transfer;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Organizacao.Dominio.Command;
using Organizacao.Dominio.Interfaces;
using ValidacaoHelper.Notification;
using WebApiOrganizacao.Configs;

namespace WebApiOrganizacao.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class OrganizacaoController : EDSController
    {
        private readonly string _aWSAcessKeyId;
        private readonly string _aWSSecretKeyId;
        private readonly IWebHostEnvironment _environment;
        private readonly string _domainName;
        private readonly string _bucketName;
        private readonly IUnitOfWorkOrganizacao _serviceQuery;
        private readonly IDomainNotificationContext _notificationContext;
        public OrganizacaoController(IMediator mediator,
            IOptions<S3ImageCronosConfig> s3ImageCronosConfig,
            IWebHostEnvironment environment, IUnitOfWorkOrganizacao serviceQuery, IDomainNotificationContext notificationContext) : base(mediator)
        {
            _serviceQuery = serviceQuery;
            _environment = environment;
            _aWSAcessKeyId = s3ImageCronosConfig.Value.AWSAcessKeyId;
            _aWSSecretKeyId = s3ImageCronosConfig.Value.AWSSecretKeyId;
            _domainName = s3ImageCronosConfig.Value.DomainName;
            _bucketName = s3ImageCronosConfig.Value.BucketName;
            _notificationContext = notificationContext;
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

        [HttpPost("CriarOrganizacao")]
        public async Task<IActionResult> CriarOrganizacao(IniciaOrganizacaoCommand command)
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
                    m => CreatedAtAction(nameof(CriarOrganizacao), new { id = m.Id }, m),
                    failed => BadRequest(failed.Errors));
                return entender;

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Conta/{idConta}")]
        public async Task<IActionResult> ObterOrganizacoesCliente(string idConta)
        {
            try
            {
                var data = await _serviceQuery.RepoOrganizacao.ObterOrganizacoes(idConta);
                if (data == null)
                {
                    return NotFound($"Não encontrado conta correspondente ao cliente");
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //public class IniciaOrganizacaoRequest
        //{
        //    public string IdConta { get; set; }
        //    public string Nome { get; set; }
        //    public string Logomarca { get; set; }
        //    public string CNPJ { get; set; }
        //}
    }
}
