using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Secrets_NET8_EF8_Blazor_VS_DEV_V1
{
    public class Secrets()
    {

        public string GetSecret(IConfiguration config, string SecretName)
        {

            string userAssignedClientId = config["Application:Id"] ?? "".ToString();
            var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions
            {
                // set the user assigned identity in Azure
                ManagedIdentityClientId = userAssignedClientId,
                // exclude Managed Identity
                ExcludeManagedIdentityCredential = true
            });

            var client = new SecretClient(
                new Uri(config["Application:KeyVaultURI"] ?? "".ToString()), credential);

            var secret = client.GetSecret(SecretName);

            return secret.Value.Value.ToString();
        }
    }
}
