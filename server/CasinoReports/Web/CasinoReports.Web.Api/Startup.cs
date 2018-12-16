namespace CasinoReports.Web.Api
{
    using System.Linq;

    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Infrastructure.Data;
    using CasinoReports.Infrastructure.Data.Seed;
    using CasinoReports.Infrastructure.Di;

    using IdentityServer4.EntityFramework.DbContexts;
    using IdentityServer4.EntityFramework.Mappers;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var applicationConnectionString = this.Configuration.GetConnectionString("ApplicationConnection");

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddApplicationServices(this.Configuration);

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // configure identity server with stores, keys, clients and scopes
            services.AddIdentityServer(options =>
                {
                    options.UserInteraction.LoginUrl = "/Identity/Account/Login";
                    options.UserInteraction.LogoutUrl = "/Identity/Account/Logout";
                })
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<ApplicationUser>()

                // this adds the config data from DB (clients, resources)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlServer(applicationConnectionString);
                })

                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlServer(applicationConnectionString);

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                this.SeedIdentityServer4Database(app);
                this.SeedApplicationDatabase(app);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            // app.UseAuthentication(); // not needed, since UseIdentityServer adds the authentication middleware
            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void SeedIdentityServer4Database(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var persistedGrantDbContext = serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
                persistedGrantDbContext.Database.Migrate();

                var configurationDbContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                configurationDbContext.Database.Migrate();

                if (!configurationDbContext.Clients.Any())
                {
                    foreach (var client in IdentityServer4Config.GetClients())
                    {
                        configurationDbContext.Clients.Add(client.ToEntity());
                    }

                    configurationDbContext.SaveChanges();
                }

                if (!configurationDbContext.IdentityResources.Any())
                {
                    foreach (var resource in IdentityServer4Config.GetIdentityResources())
                    {
                        configurationDbContext.IdentityResources.Add(resource.ToEntity());
                    }

                    configurationDbContext.SaveChanges();
                }

                if (!configurationDbContext.ApiResources.Any())
                {
                    foreach (var resource in IdentityServer4Config.GetApiResources())
                    {
                        configurationDbContext.ApiResources.Add(resource.ToEntity());
                    }

                    configurationDbContext.SaveChanges();
                }
            }
        }

        private void SeedApplicationDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                DatabaseInitializer.SeedDatabase(applicationDbContext, userManager, roleManager);
            }
        }
    }
}
