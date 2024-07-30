using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chatter.Infrastructure.Extensions;

public static class KeyVaultExtension
{
    private static ClientSecretCredential  GetCredential(IConfiguration configuration)
    {
        var keyVaultClientId = configuration.GetSection("KeyVault:ClientId");
        var keyVaultClientSecret = configuration.GetSection("KeyVault:ClientSecret");
        var keyVaultDirectoryId = configuration.GetSection("KeyVault:DirectoryId");
        
        return new ClientSecretCredential(keyVaultDirectoryId.Value!,
            keyVaultClientId.Value!, keyVaultClientSecret.Value!);
    }

    public static string GetConnectionSecret(IConfiguration configuration, string name)
    {
        var keyVaultUrl = configuration.GetSection("KeyVault:KeyVaultUrl");

        var credential = GetCredential(configuration);
        var client = new SecretClient(new Uri(keyVaultUrl.Value!), credential);
        return client.GetSecret(name).Value.Value;
    }
}