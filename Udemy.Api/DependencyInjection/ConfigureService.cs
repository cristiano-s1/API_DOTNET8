using Microsoft.Extensions.DependencyInjection.Extensions;
using Udemy.Api.Services;
using Udemy.Api.Services.Implementations;

namespace Udemy.Api.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITokenService, TokenServiceImplementation>();

            //Files
            serviceCollection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
        }
    }
}
