using System.Reflection;
using Citrouille.Data;
using Citrouille.Infrastructure.Commands;
using Citrouille.Infrastructure.Commands.Factories;
using Citrouille.Infrastructure.Commands.Models;
using Citrouille.Infrastructure.Helpers;
using Citrouille.Infrastructure.Queries;
using Citrouille.Infrastructure.Services.FullTextSearch;
using Citrouille.Infrastructure.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Citrouille.Infrastructure;

public static class StartupExtensions
{
    private static IElasticClient _elasticClient;
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICollectionReadService, CollectionReadService>();
        services.AddScoped<ICollectionThemeReadService, CollectionCollectionThemeReadService>();
        services.AddScoped<ICollectionTagReadService, CollectionCollectionTagReadService>();

        var options = configuration.GetSection("MySql").Get<DbOptions>();
        services.AddDbContext<CollectionDbContext>(ctx => 
            ctx.UseMySql(
                options.ConnectionString,
                new MySqlServerVersion(new Version(8, 0, 29)),
                o => o.SchemaBehavior(MySqlSchemaBehavior.Translate, (schema, table) => $"{schema}_{table}")
                ));

        services.AddScoped<CollectionQueryService>();
        services.AddFullTextSearch<CollectionFullTextSearchModel>(configuration);
        services.AddAutoMapperProfile(Assembly.GetExecutingAssembly());
        
        return services;
    }
    
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ICollectionFactory, CollectionFactory>();
        services.AddScoped<ICollectionCommandService, CollectionCommandService>();
        services.AddScoped<CreateCollectionService>();
        
        return services;
    }

    public static IServiceCollection AddFullTextSearch<TModel>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TModel : class
    {
        var indexName = $"{typeof(TModel).Name.ToLower()}_index";

        if (_elasticClient == null)
        {
            var connectionString = configuration.GetValue<string>("ElasticSearchConnection");

            var node = new Uri(connectionString);
            var settings = new ConnectionSettings(node)
                .DefaultMappingFor<TModel>(s => s.IndexName(indexName));

            _elasticClient = new ElasticClient(settings);
        }

        _elasticClient.Indices.Create(
            indexName,
            index => index
                .Map<TModel>(p => p.AutoMap()));

        services.AddSingleton(_elasticClient);
        services.AddTransient<IFullTextSearchService, FullTextSearchService>();
        
        return services;
    }

    public static IServiceCollection AddAutoMapperProfile(
            this IServiceCollection services, Assembly assembly)
        => services.AddAutoMapper((_, config) => config
            .AddProfile(new MappingProfile(assembly)),
            Array.Empty<Assembly>());
}