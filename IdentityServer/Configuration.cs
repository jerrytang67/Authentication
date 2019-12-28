using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Phone(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
            new IdentityResource {Name = "rc.scope", UserClaims = {"rc.garndma"}},
            new IdentityResource {Name = "rc.api.scope", UserClaims = {"rc.api.garndma"}},

        };

        public static IEnumerable<ApiResource> GetApis() => new List<ApiResource>
        {
            new ApiResource("ApiOne", new[] {"rc.api.garndma"}),
            new ApiResource("ApiTwo")
        };

        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            new Client
            {
                ClientId = "client_id",
                ClientSecrets = {new Secret("client_secret".ToSha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"ApiOne"}
            },
            new Client()
            {
                ClientId = "client_id_mvc",
                ClientSecrets = {new Secret("client_secret_mvc".ToSha256())},
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = {"https://localhost:44382/signin-oidc"},
                AllowedScopes =
                {
                    "ApiOne",
                    "ApiTwo",
                    "openid",
                    "profile",
                    "rc.scope"
                },

                // puts all the claims in the id_token
                AlwaysIncludeUserClaimsInIdToken = true,
                RequireConsent = false,

                AllowOfflineAccess =  true
            },
            new Client()
            {
                ClientId = "client_id_js",

                AllowedGrantTypes = GrantTypes.Implicit,

                AllowedCorsOrigins = { "https://localhost:44314" },
                RedirectUris = {"https://localhost:44314/home/signin"},

                AllowedScopes =
                {
                    "openid",
                    "ApiOne",
                    "ApiTwo",
                    "rc.scope"
                },
                AccessTokenLifetime = 10,
                AllowAccessTokensViaBrowser = true,
                RequireConsent = false,
            }
        };
    }
}