namespace CasinoReports.Web.Api
{
    using System.Collections.Generic;

    using IdentityServer4;
    using IdentityServer4.Models;

    public class IdentityServer4Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResources.Phone(),
                new IdentityResource("roles", new List<string> { "role" }),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("CasinoReportsAPI", "Casino Reports API")
                {
                    UserClaims = { "role" },
                },
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                // http://docs.identityserver.io/en/release/reference/client.html
                new Client
                {
                    ClientId = "AngularSPA",
                    ClientName = "Angular SPA Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false,

                    RequireConsent = false,

                    AccessTokenLifetime = 3600, // Lifetime of access token in seconds

                    RedirectUris = { "http://localhost:5001", "http://localhost:5001/silent-refresh.html" },
                    PostLogoutRedirectUris = { "http://localhost:5001/logout" },
                    AllowedCorsOrigins = { "http://localhost:5001" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        "roles",
                        "CasinoReportsAPI",
                    },
                },
            };
        }
    }
}
