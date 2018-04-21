using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using SXA.Foundation.SecurityHeaders.Context;

namespace SXA.Foundation.SecurityHeaders.DI
{
    public class Configurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISecurityHeadersSiteContext, SecurityHeadersSiteContext>();
        }
    }
}