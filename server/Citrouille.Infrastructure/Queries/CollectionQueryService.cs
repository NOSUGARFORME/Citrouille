using Citrouille.Data;
using Citrouille.Data.Entities;
using Citrouille.Infrastructure.Queries.Models;
using Microsoft.EntityFrameworkCore;

namespace Citrouille.Infrastructure.Queries;

public class CollectionQueryService
{
    private readonly DbSet<Collection> _collections;
    
    public CollectionQueryService(CollectionDbContext context)
        => _collections = context.Collections;

    public Task<CollectionDto> GetById(Guid id)
    {
        return _collections
            .Include(c => c.Fields)
            .Include(c => c.Items)
            .ThenInclude(i => i.Fields)
            .Include(c => c.Tags)
            .Include(c => c.Theme)
            .Where(c => c.Id == id)
            .Select(c => c.AsDto())
            .AsNoTracking()
            .SingleOrDefaultAsync();
    }
}