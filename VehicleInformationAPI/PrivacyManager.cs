using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;

namespace VehicleInformationAPI
{
    public class PrivacyManager(string prefix) : KeyVaultSecretManager
    {
        private readonly string _prefix = $"{prefix}-";


        public override bool Load(SecretProperties secret)
        {
            return secret.Name.StartsWith(prefix);
        }

        public override string GetKey(KeyVaultSecret secret)
        {
            return secret.Name[_prefix.Length..].Replace("--", ConfigurationPath.KeyDelimiter);
        }
    }
}
