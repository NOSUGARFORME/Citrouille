using Citrouille.Data.Entities;

namespace Citrouille.Infrastructure.Commands;

public interface ICollectionThemeReadService
{
    Task<CollectionTheme> GetByIdAsync(Guid id);
}