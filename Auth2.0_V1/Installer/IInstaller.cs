using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth20_V1.Installer
{
    public interface IInstaller
    {
        void InstallerService(IServiceCollection services, IConfiguration configuration);
    }
}
