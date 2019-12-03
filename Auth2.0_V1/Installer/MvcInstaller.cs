using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Abstract;
using App.Core.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Auth20_V1.Installer
{
    public class MvcInstaller : IInstaller
    {
        public void InstallerService(IServiceCollection services, IConfiguration configuration)
        {
            //  init the JwtSetting instance and map configration with configuration varilabe 
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            //  DI for IdenityServices Class 
            services.AddScoped<IIdentityServices, IdentityServices>();
            services.AddScoped<IManager, Manager>();
            services.AddMvc();

            // TokenValidator Parameter 
            var tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidIssuer = "https://localhost:5000/",
                ValidAudience = "demo",
                ValidateIssuer = true,
                ValidateAudience = true,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddSingleton(tokenValidationParameters);

            // add defualt authentication JwtBearerDefaults
            services.AddAuthentication(configureOptions: x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });

            // add authorization policy  
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorRole",
                     policy => policy.RequireRole("Administrator"));
            });
        }
    }
}
