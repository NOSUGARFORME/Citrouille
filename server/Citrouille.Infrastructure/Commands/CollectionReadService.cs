using Citrouille.Data;
using Citrouille.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Citrouille.Infrastructure.Commands;

public class CollectionReadService : ICollectionReadService
{
    private readonly DbSet<Collection> _collections;

    public CollectionReadService(CollectionDbContext context)
        => _collections = context.Collections;

    public Task<bool> ExistsByTitleAsync(string title)
        => _collections.AnyAsync(c => c.Title == title);
}