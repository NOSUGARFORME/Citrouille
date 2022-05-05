using Citrouille.Data;
using Citrouille.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Citrouille.Infrastructure.Commands;

public class CollectionCollectionThemeReadService : ICollectionThemeReadService
{
    private readonly DbSet<CollectionTheme> _themes;

    public CollectionCollectionThemeReadService(CollectionDbContext context)
        => _themes = context.Themes;


    public Task<CollectionTheme> GetByIdAsync(Guid id)
        => _themes
            .Where(t => t.Id.Equals(id))
            .SingleOrDefaultAsync();
}