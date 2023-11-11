using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;

namespace ServiceImagemOrganizacao
{
    public class ServiceImage
    {
        string _aWSAcessKeyId, _aWSSecretKeyId, _domainName;

        public ServiceImage(string aWSAcessKeyId, string aWSSecretKeyId, string domainName)
        {
            _aWSAcessKeyId = aWSAcessKeyId;
            _aWSSecretKeyId = aWSSecretKeyId;   
            _domainName = domainName;
        }

        public async Task<string> UploadLogomarca(List<IFormFile> files, string nome, string bucketName)
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
                        Key = nome + ".png",
                        BucketName = bucketName,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }

                var resp = _domainName + "/" + nome + ".png";
                return resp;

            }
            catch (Exception ex)
            {
                 throw new Exception("Imagem não foi salva", ex);
            }
        }
    }
}