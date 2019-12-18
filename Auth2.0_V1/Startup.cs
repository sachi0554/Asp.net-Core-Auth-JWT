using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Auth20_V1.Installer;
using Microsoft.IdentityModel.Logging;
using Auth20_V1.customhandler;
using Auth20_V1.Middleware;

namespace Auth20_V1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // IInstaller class inject the services 
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
               typeof(IService).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IService>().ToList();

            installers.ForEach(installer => installer.InstallerService(services, Configuration));
             
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }

           
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"), appBuilder =>
            {
                appBuilder.UseMiddleware<RedisTokenValidation>();
            });
          //  app.UseMiddleware<RedisTokenValidation>();
            app.UseAuthentication();

            // swagger configruation 
            var swaggerOptions = new SettingMappers.SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger();

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
            });
            // end swagger configruation 
            app.UseMvc();


        }
    }
}
