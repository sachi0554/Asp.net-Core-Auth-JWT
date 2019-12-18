
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using App.Core.Contract;
using App.DataAccess;
using App.Core.Model;

namespace Auth20_V1.Installer
{
    public class DbServices : IService
    {
        public void InstallerService(IServiceCollection services, IConfiguration configuration)
        {
            // context Register on IServiceCollection 

            services.AddDbContext<ApplicationContext>(options =>
              options.UseSqlServer(
                  configuration.GetConnectionString("DefaultConnection")));

            //Register Idenity Services 
            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
               .AddDefaultTokenProviders();
        }
    }
}
