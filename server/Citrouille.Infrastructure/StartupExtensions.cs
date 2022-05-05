using Citrouille.Data;
using Citrouille.Infrastructure.Commands;
using Citrouille.Infrastructure.Commands.Factories;
using Citrouille.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Citrouille.Infrastructure;

public static class StartupExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICollectionReadService, CollectionReadService>();

        var options = configuration.GetSection("MySql").Get<DbOptions>();
        services.AddDbContext<CollectionDbContext>(ctx => 
            ctx.UseMySql(
                options.ConnectionString,
                new MySqlServerVersion(new Version(8, 0, 29)),
                o => o.SchemaBehavior(MySqlSchemaBehavior.Translate, (schema, table) => $"{schema}_{table}")
                ));

        services.AddScoped<CollectionQueryService>();
        // services.AddSingleton<IWeatherService, DumbWeatherService>();

        return services;
    }
    
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ICollectionFactory, CollectionFactory>();
        services.AddScoped<ICollectionCommandService, CollectionCommandService>();

        return services;
    }
}