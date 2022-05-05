using Citrouille.Data;
using Citrouille.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Citrouille.Infrastructure.Commands;

public class CollectionCollectionTagReadService : ICollectionTagReadService
{
    private readonly DbSet<Tag> _tags;

    public CollectionCollectionTagReadService(CollectionDbContext context)
        => _tags = context.Tags;

    public Task<Tag> GetByNameAsync(string name)
        => _tags
            .Where(t => t.Name == name)
            .SingleOrDefaultAsync();
}