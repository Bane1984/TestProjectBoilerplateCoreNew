using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.IdentityServer4;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using TestProjectBoilerplateCore.Authorization.Users;
using TestProjectBoilerplateCore.EntityFrameworkCore;
using TestProjectBoilerplateCore.Identity;
using TestProjectBoilerplateCore.IdentityServer4.Models;
using Resources = TestProjectBoilerplateCore.IdentityServer4.Models.Resources;

namespace TestProjectBoilerplateCore.IdentityServer4
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            IdentityRegistrar.Register(services);

            services.AddMvc();

            services.AddIdentityServer()
                .AddInMemoryClients(Clients.Get())
                .AddInMemoryIdentityResources(Resources.GetIdentityResources()) 
                .AddInMemoryApiResources(Resources.GetApiResources())
                .AddDeveloperSigningCredential()
                .AddAbpPersistedGrants<TestProjectBoilerplateCoreDbContext>() //dodamo Abp-ov context koji se nalazi u 
                .AddAbpIdentityServer<User>();


            //dodati Nuget paket Abp.Castle.Log4Net za UseAbpLog4Net - u njemu je definisana metoda UseAbpLog4Net()
            return services.AddAbp<TestProjectBoilerplateCoreWebCoreModule>(
                options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                )
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbp();

            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
