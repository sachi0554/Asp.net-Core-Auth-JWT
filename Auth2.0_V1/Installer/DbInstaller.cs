
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using App.Domain;
using Microsoft.AspNetCore.Identity;
using App.Domain.Model;
using System;

namespace Auth20_V1.Installer
{
    public class DbInstaller : IInstaller
    {
        public void InstallerService(IServiceCollection services, IConfiguration configuration)
        {
           
            services.AddDbContext<ApplicationContext>(options =>
              options.UseSqlServer(
                  configuration.GetConnectionString("DefaultConnection")));
            /*  services.AddIdentity<ApplicationUser, IdentityRole>()*/
          
            /*   services.AddIdentityCore<ApplicationUser>()*/
            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
               .AddDefaultTokenProviders();
          

        }
    }
}
