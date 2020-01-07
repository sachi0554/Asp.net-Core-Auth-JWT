using System;
using System.Text;
using App.Core.Abstract;
using App.Core.Contract;
using App.DataAccess;
using Auth20_V1.filter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Auth20_V1.Installer
{
    public class MvcServices : IService
    {
        public void InstallerService(IServiceCollection services, IConfiguration configuration)
        {
            //  init the JwtSetting instance and map configration with configuration varilabe 
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            //  DI for IdenityServices Class 
            services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));
            services.AddScoped<IIdentityServices, IdentityServices>();
            services.AddScoped<IManager, Manager>();
         //   services.AddSingleton<IAuthorizationHandler, CustomHandler>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                builder =>
                {
                    builder.WithOrigins("https://localhost:5000/")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()  
                                        .AllowCredentials();
                });
            });
            services.AddMvc(options =>
              {
                  options.Filters.Add<ValidationFilter>();
                  
              });
       
            // TokenValidator Parameter 
            var tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
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
                options.AddPolicy("Crud", policy => policy.RequireClaim("Create"));
            });

            
        }
    }
}
