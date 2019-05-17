using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer4Abp.Models
{
    internal class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {

                new Client
                {
                    ClientId = "oauthClient",
                    ClientName = "Example Client Credentials Client Application",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("superSecretPassword".Sha256())
                    },
                    AllowedScopes = new List<string> {"customAPI.read"}
                },

                //new Client {
                //    ClientId = "openIdConnectClient",
                //    ClientName = "Example Implicit Client Application",
                //    AllowedGrantTypes = GrantTypes.Implicit,
                //    AllowedScopes = new List<string>
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        IdentityServerConstants.StandardScopes.Email,
                //        "role",
                //        "customAPI.write"
                //    },
                //    RedirectUris = new List<string> {"http://localhost:50486/signin-oidc"},
                //    PostLogoutRedirectUris = new List<string> { "http://localhost:50486" }
                //}

                new Client
                {
                    ClientId = "TestProjectAbp",
                    ClientName = "Abp Host",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    ClientSecrets = {new Secret("secret".Sha256())},

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email
                    },

                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    RedirectUris = {"http://localhost:21021/signin-oidc"},
                    PostLogoutRedirectUris = {"http://localhost:21021/signout-callback-oidc"},
                    FrontChannelLogoutUri = "http://localhost:21021/signout-oidc"

                }
            };
        }
    }
}
