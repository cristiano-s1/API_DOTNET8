using Microsoft.EntityFrameworkCore;
using Udemy.Api.Context;
using Udemy.Api.Repository;
using Udemy.Api.Repository.Generic;

namespace Udemy.Api.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection, string connection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            serviceCollection.AddScoped<IPersonRepository, PersonRepository>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();

            //Connection Database
            serviceCollection.AddDbContext<UdemyContext>(options => options.UseSqlServer(connection,
                sqlServerOptions => sqlServerOptions.CommandTimeout(300000)));
        }
    }
}
