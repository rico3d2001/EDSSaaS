using Amazon.S3;
using Amazon.S3.Transfer;
using EDSCore;
using Hubs.Dominio.Commands;
using Hubs.Dominio.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceFotoUsuario;
using ValidacaoHelper.Notification;

namespace APIGatewayEDS.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]/v1")]
    public class CustomerController : EDSController
    {
        private readonly string _aWSAcessKeyId;
        private readonly string _aWSSecretKeyId;
        private readonly string _domainName;
        private readonly string _bucketName;
        private readonly IUnitOfWorkHub _serviceQuery;
        private readonly IDomainNotificationContext _notificationContext;

        public CustomerController(IMediator mediator,
            IOptions<S3ImageCronosConfig> s3ImageCronosConfig, HttpClient httpClient,
            IDomainNotificationContext notificationContext, IUnitOfWorkHub serviceQuery) : base(mediator, httpClient)
        {
            _aWSAcessKeyId = s3ImageCronosConfig.Value.AWSAcessKeyId;
            _aWSSecretKeyId = s3ImageCronosConfig.Value.AWSSecretKeyId;
            _domainName = s3ImageCronosConfig.Value.DomainName;
            _bucketName = s3ImageCronosConfig.Value.BucketName;
            _notificationContext = notificationContext;
            _serviceQuery = serviceQuery;
        }

        [HttpPut("UploadFotoUsuario/{email}")]
        [DisableRequestSizeLimit]
        public async Task<Ok<string>> UploadFotoUsuario(List<IFormFile> files, string email)
        {
            try
            {
                using (var client = new AmazonS3Client(_aWSAcessKeyId, _aWSSecretKeyId, Amazon.RegionEndpoint.USEast1))
                {
                    using var newMemoryStream = new MemoryStream();
                    files[0].CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = email + ".png",
                        BucketName = _bucketName,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }

                var resp = _domainName + "/" + email + ".png";
                return TypedResults.Ok(resp);

            }
            catch (Exception)
            {
                return null;
            }
        }

        ///[AllowAnonymous]
        [HttpGet("GetCustomersByHub/{email}")]
        public async Task<IActionResult> GetCustomersByHub(string email)
        {
            try
            {
                var hub = await _serviceQuery.HubRepositorio.GetByEmail(email);
                var data = hub.Customers;

                if (data == null)
                {
                    return NotFound("Usuário não cadastrado");
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        //[AllowAnonymous]
        [HttpGet("GetByEmail/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                var hub = await _serviceQuery.HubRepositorio.GetByEmail(email);
                var data = hub.Customers.First();
                if (data == null)
                {
                    return NotFound($"Não encontrado usuário com o e-mail: {email}");
                }



                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //[AllowAnonymous]
        [HttpPut("ConfirmaHub")]
        public async Task<IActionResult> ConfirmaHub([FromBody] ConfirmaCustomerCommand command)
        {
            try
            {

                var result = await _mediator.Send(command);

                if (_notificationContext.HasErrorNotifications)
                {
                    var notifications = _notificationContext.GetErrorNotifications();
                    var message = string.Join(", ", notifications.Select(x => x.Value));
                    var erros = new List<ValidationFalha>(); erros.Add(new ValidationFalha("500", message));
                    return BadRequest(new ValidationFalhas(erros));
                }

                return result.Match<IActionResult>(
                   m => CreatedAtAction(nameof(ConfirmaHub), new { id = m.Id }, m),
                   failed => BadRequest(failed.Errors));
            }
            catch (Exception)
            {
                return BadRequest("Não foram inseridos novos dados no Hub");
            }
        }

    }
}
