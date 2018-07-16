using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using SXA.Feature.SecurityHeaders.Context;

namespace SXA.Feature.SecurityHeaders.DI
{
    public class Configurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISecurityHeadersSiteContext, SecurityHeadersSiteContext>();
        }
    }
}