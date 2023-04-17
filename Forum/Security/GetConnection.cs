using Google.Cloud.SecretManager.V1;

namespace Forum.Security
{
    public class GetConnection
    {
        public String AccessSecretVersion(
        string projectId = "132122417804", string secretId = "CONNECTION", string secretVersionId = "1")
        {
            SecretManagerServiceClient client = SecretManagerServiceClient.Create();

            SecretVersionName secretVersionName = new SecretVersionName(projectId, secretId, secretVersionId);

            AccessSecretVersionResponse result = client.AccessSecretVersion(secretVersionName);

            String payload = result.Payload.Data.ToStringUtf8();
            return payload;
        }
    }
}
