using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.IdentityServer4;
using Castle.Facilities.Logging;
using IdentityServer4Setup.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using TestProjectBoilerplateCore.Authorization.Users;
using TestProjectBoilerplateCore.EntityFrameworkCore;
using TestProjectBoilerplateCore.Identity;

namespace TestProjectBoilerplateCore.IdentityServer4
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            IdentityRegistrar.Register(services);

            //services.AddIdentityServer()
            //    .AddInMemoryClients(Clients.Get())
            //    .AddInMemoryIdentityResources(Resources.GetIdentityResources())
            //    .AddInMemoryApiResources(Resources.GetApiResources())
            //    .AddTestUsers(Users.Get())
            //    .AddDeveloperSigningCredential();

            services.AddIdentityServer()
                .AddInMemoryClients(Clients.Get())
                .AddInMemoryIdentityResources(Resources.GetIdentityResources())
                .AddInMemoryApiResources(Resources.GetApiResources())
                .AddAbpPersistedGrants<TestProjectBoilerplateCoreDbContext>()
                .AddAbpIdentityServer<User>()
                //.AddTestUsers(IdentityServer4Setup.Models.Users.Get())
                .AddDeveloperSigningCredential();

            //dodati Nuget paket Abp.Castle.Log4Net za UseAbpLog4Net - u njemu je definisana metoda UseAbpLog4Net()
            return services.AddAbp<TestProjectBoilerplateCoreIdentityServer4Module>(
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
