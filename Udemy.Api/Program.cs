using EvolveDb;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Data.SqlClient;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;
using Udemy.Api.DependencyInjection;
using Udemy.Api.Hypermedia.Enricher;
using Udemy.Api.Hypermedia.Filters;

var builder = WebApplication.CreateBuilder(args);
var appName = "REST APIs RESTful from 0 to Azure with .NET 8 and Docker";
var appDescription = $"REST API RESTful developed in course {appName}";
var appVersion = "v1";

builder.Services.AddRouting(options => options.LowercaseUrls = true);

#region CORS
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));
#endregion

#region CONTROLLERS
builder.Services.AddControllers();
#endregion

#region SWAGGER
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(appVersion,
    new OpenApiInfo
    {
        Title = appName,
        Version = appVersion,
        Description = appDescription,
        Contact = new OpenApiContact
        {
            Name = "Cristiano Souza",
            Url = new Uri("https://github.com/cristiano-s1/CourseUdemy")
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using o Bearer scheme. Example: \"Authorization: Bearer {token}\""
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    //Include comments
    var xmlFile = "Udemy.Api.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
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

#region FORMATER FOR JSON OR XML
builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
})
.AddXmlSerializerFormatters();
#endregion

#region HYPER MEDIA
var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEnricherList.Add(new BookEnricher());
builder.Services.AddSingleton(filterOptions);
#endregion

#region VERSIONING API
builder.Services.AddApiVersioning();
#endregion

#region DEPENDENCY INJECTION
ConfigureBusiness.ConfigureDependenciesBusiness(builder.Services);
ConfigureRepository.ConfigureDependenciesRepository(builder.Services, connection);
#endregion

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors();

app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appVersion}: Development");
    });
}

if (app.Environment.IsProduction())
{
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appVersion}: Production");
        c.DefaultModelsExpandDepth(-1);
    });
}

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}"); //Route hypermedia

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
