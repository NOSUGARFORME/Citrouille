using Citrouille.Domain.Factories;
using Citrouille.Shared.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Citrouille.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCommands();
        services.AddSingleton<ICollectionFactory, CollectionFactory>();
        
        return services;
    }
}