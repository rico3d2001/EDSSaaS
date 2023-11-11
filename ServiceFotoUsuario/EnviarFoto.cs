using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;

namespace ServiceFotoUsuario
{
    public class EnviarFoto
    {
        string _aWSAcessKeyId;
        string _aWSSecretKeyId;
        string _bucketName;
        public EnviarFoto(string aWSAcessKeyId, string aWSSecretKeyId, string bucketName)
        {
            _aWSAcessKeyId = aWSAcessKeyId;
            _aWSSecretKeyId = aWSSecretKeyId;
            _bucketName = bucketName;
        }

        public async Task Enviar(IFormFile file, string email)
        {
            try
            {
                using (var client = new AmazonS3Client(_aWSAcessKeyId, _aWSSecretKeyId, Amazon.RegionEndpoint.USEast1))
                {
                    using (var newMemoryStream = new MemoryStream())
                    {
                        file.CopyTo(newMemoryStream);

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
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
