using Microsoft.Extensions.DependencyInjection;
using SSCMS.Database.Abstractions;
using SSCMS.Database.Core;
using SSCMS.Plugins;

namespace SSCMS.Database
{
    public class Startup : IPluginConfigureServices
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDatabaseManager, DatabaseManager>();
        }
    }
}
