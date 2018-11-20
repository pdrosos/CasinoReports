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
                    RequireClientSecret = false,

                    RequireConsent = false,

                    AccessTokenLifetime = 3600, // Lifetime of access token in seconds

                    RedirectUris = { "http://localhost:5001/callback" },
                    PostLogoutRedirectUris = { "http://localhost:5001/home" },
                    AllowedCorsOrigins = { "http://localhost:5001" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "CasinoReportsAPI",
                    },
                },
            };
        }
    }
}
