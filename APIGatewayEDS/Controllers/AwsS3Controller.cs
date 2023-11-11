using Amazon.S3.Transfer;
using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceFotoUsuario;

namespace APIGatewayEDS.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class AwsS3Controller : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string _aWSAcessKeyId;
        private readonly string _aWSSecretKeyId;
        private readonly string _domainName;
        private readonly string _bucketName;

        public AwsS3Controller(IOptions<S3ImageCronosConfig> s3ImageCronosConfig, IWebHostEnvironment environment)
        {
            _environment = environment;
            _aWSAcessKeyId = s3ImageCronosConfig.Value.AWSAcessKeyId;
            _aWSSecretKeyId = s3ImageCronosConfig.Value.AWSSecretKeyId;
            _domainName = s3ImageCronosConfig.Value.DomainName;
            _bucketName = s3ImageCronosConfig.Value.BucketName;
        }

        [HttpPost("UploadFotoUsuario")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFotoUsuario(IFormFile file, string email)
        {
            //Para varios é IFormFileCollection com o recurso do TransferUtility de multiplos

            try
            {

                EnviarFoto enviarFoto = new EnviarFoto(_aWSAcessKeyId, _aWSSecretKeyId, _bucketName);



                return Ok(_domainName + "/" + email + ".png");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost("DownloadFotoUsuario")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> DownloadFotoUsuario(string arquivoOrigem, string pastaDestino)
        {

            //Para varios é IFormFileCollection
            //Resultado resp = null;
            try
            {
                using (var client = new AmazonS3Client(_aWSAcessKeyId, _aWSSecretKeyId, Amazon.RegionEndpoint.USEast1))
                {

                    //RECURSO PARA BUSCAR EM PASTAS
                    //var listObjectsRequest = new ListObjectsV2Request()
                    //{
                    //BucketName = _bucketName
                    //};
                    //var response = await client.ListObjectsV2Async(listObjectsRequest);

                    var fileTransferUtility = new TransferUtility(client);


                    fileTransferUtility.Download($"{pastaDestino}\\{arquivoOrigem}", _bucketName, arquivoOrigem);


                    return Ok(Path.Combine(pastaDestino, arquivoOrigem));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }

}
