using EvolveDb;
using Microsoft.Data.SqlClient;
using Serilog;
using Udemy.Api.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

#region CONTROLLERS
builder.Services.AddControllers();
#endregion

#region CONNECTION STRING
var connection = builder.Configuration.GetConnectionString("Default");
#endregion

#region MIGRATIONS
if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connection);
}
#endregion

#region VERSIONING API
builder.Services.AddApiVersioning();
#endregion

#region DEPENDENCY INJECTION
ConfigureBusiness.ConfigureDependenciesBusiness(builder.Services);
ConfigureRepository.ConfigureDependenciesRepository(builder.Services, connection);
#endregion

#region CORS
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));
#endregion

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#region EVOLVE AND LOGS
static void MigrateDatabase(string connection)
{
    try
    {
        var evolveConnection = new SqlConnection(connection);

        var evolve = new Evolve(evolveConnection, Log.Information)
        {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true,
        };

        evolve.Migrate();
    }
    catch (Exception ex)
    {
        Log.Error("Database migration failed", ex);
        throw;
    }
}
#endregion
