﻿using System.Collections.Generic;
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
            new IdentityResources.Profile()
        };

        public static IEnumerable<ApiResource> GetApis() => new List<ApiResource> {new ApiResource("ApiOne"), new ApiResource("ApiTwo")};

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
                    "profile"
                },
                RequireConsent = false
            }
        };
    }
}