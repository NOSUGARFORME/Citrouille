using System.Reflection;
using System.Text;
using Citrouille.Data;
using Citrouille.Data.Entities;
using Citrouille.Data.Seed;
using Citrouille.Infrastructure.Commands;
using Citrouille.Infrastructure.Commands.Factories;
using Citrouille.Infrastructure.Commands.Models;
using Citrouille.Infrastructure.Helpers;
using Citrouille.Infrastructure.Queries;
using Citrouille.Infrastructure.Services.FullTextSearch;
using Citrouille.Infrastructure.Services.FullTextSearch.Models;
using Citrouille.Infrastructure.Services.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nest;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Citrouille.Infrastructure;

public static class StartupExtensions
{
    private static IElasticClient _elasticClient;
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<ICollectionReadService, CollectionReadService>()
            .AddScoped<ICollectionThemeReadService, CollectionCollectionThemeReadService>()
            .AddScoped<ICollectionTagReadService, CollectionCollectionTagReadService>()
            .AddScoped<CollectionQueryService>()
            .AddTransient<IIdentityService, IdentityService>()
            .AddTransient<ITokenGeneratorService, TokenGeneratorService>()
            .AddTransient<IDataSeeder, IdentityDataSeeder>();

        services
            .AddDatabase(configuration)
            .AddApplicationSettings(configuration)
            .AddFullTextSearch<CollectionFullTextSearchModel>(configuration)
            .AddAutoMapperProfile(Assembly.GetExecutingAssembly())
            .AddTokenAuthentication(configuration)
            .AddUserStorage();
        
        return services;
    }
    
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ICollectionFactory, CollectionFactory>();
        services.AddScoped<ICollectionCommandService, CollectionCommandService>();
        services.AddScoped<CreateCollectionService>();
        
        return services;
    }
    
    public static IApplicationBuilder Initialize(
        this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;
        
        var seeders = serviceProvider.GetServices<IDataSeeder>();

        foreach (var seeder in seeders)
        {
            seeder.SeedData();
        }

        return app;
    }

    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var options = configuration.GetSection("MySql").Get<DbOptions>();
        return services.AddDbContext<CollectionDbContext>(ctx => 
            ctx.UseMySql(
                options.ConnectionString,
                new MySqlServerVersion(new Version(8, 0, 29)),
                o => o.SchemaBehavior(MySqlSchemaBehavior.Translate, (schema, table) => $"{schema}_{table}")
            ));
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
    
    public static IServiceCollection AddApplicationSettings(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .Configure<ApplicationSettings>(
                configuration.GetSection(nameof(ApplicationSettings)), 
                config => config.BindNonPublicProperties = true);
    
    public static IServiceCollection AddTokenAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var secret = configuration
            .GetSection(nameof(ApplicationSettings))
            .GetValue<string>(nameof(ApplicationSettings.Secret));

        var key = Encoding.ASCII.GetBytes(secret);

        services
            .AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearer =>
            {
                bearer.RequireHttpsMetadata = false;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
    
    public static IServiceCollection AddUserStorage(
        this IServiceCollection services)
    {
        services
            .AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<CollectionDbContext>();

        return services;
    }
}
