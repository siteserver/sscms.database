using Microsoft.Extensions.DependencyInjection;
using SSCMS.Database.Abstractions;
using SSCMS.Plugins;

namespace SSCMS.Database.Implements
{
    public class PluginConfigureServices : IPluginConfigureServices
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDatabaseManager, DatabaseManager>();
        }
    }
}
