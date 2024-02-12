using Udemy.Api.Business;
using Udemy.Api.Business.Implementations;

namespace Udemy.Api.DependencyInjection
{
    public class ConfigureBusiness
    {
        public static void ConfigureDependenciesBusiness(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            serviceCollection.AddScoped<IBookBusiness, BookBusinessImplementation>();
            serviceCollection.AddScoped<ILoginBusiness, LoginBusinessImplementation>();
            serviceCollection.AddScoped<IFileBusiness, FileBusinessImplementation>();
        }
    }
}
