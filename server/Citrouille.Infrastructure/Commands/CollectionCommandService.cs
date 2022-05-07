using Citrouille.Data;
using Citrouille.Data.Entities;
using Citrouille.Infrastructure.Commands.Models;
using Citrouille.Infrastructure.Services.FullTextSearch;
using Citrouille.Infrastructure.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Citrouille.Infrastructure.Commands;

public sealed class CollectionCommandService : ICollectionCommandService
{
    private readonly CollectionDbContext _writeDbContext;
    private readonly IFullTextSearchService _fullTextSearch;

    public CollectionCommandService(CollectionDbContext writeDbContext, IFullTextSearchService fulltextSearch)
    {
        _writeDbContext = writeDbContext;
        _fullTextSearch = fulltextSearch;
    }

    Task<Collection> GetAsync(Guid id)
    {
        return _writeDbContext.Collections
            .Include(c => c.Items)
            .Include(c => c.Theme)
            .SingleOrDefaultAsync(c => c.Id == id);
    }
    
    public async Task CreateCollectionAsync(Collection collection)
    {
        collection.ThrowIfInvalid();

        await _writeDbContext.Collections.AddAsync(collection);
        await _writeDbContext.SaveChangesAsync();
    }

    public async Task AddCollectionItemAsync(AddCollectionItem command)
    {
        var collection = await GetAsync(command.CollectionId);

        if (collection is null)
        {
            throw new CollectionNotFoundException(command.CollectionId);
        }

        var item = new CollectionItem(command.Name, command.Fields);
        collection.AddItem(item);

        await UpdateCollectionAsync(collection);
    }

    public async Task UpdateCollectionAsync(Collection collection)
    {
        collection.ThrowIfInvalid();

        _writeDbContext.Collections.Update(collection);
        await _writeDbContext.SaveChangesAsync();
        await _fullTextSearch.Update(new CollectionFullTextSearchModel
            {
                Id = collection.Id,
                Description = collection.Description
            },
            collection.Id);
    }

    public async Task UpdateCollectionItemAsync(UpdateCollectionItem command)
    {
        var collection = await GetAsync(command.CollectionId);

        if (collection is null)
        {
            throw new CollectionNotFoundException(command.CollectionId);
        }
        
    }

    public async Task RemoveCollectionAsync(RemoveCollection command)
    {
        var collection = await GetAsync(command.Id);

        if (collection is null)
        {
            throw new CollectionNotFoundException(command.Id);
        }

        _writeDbContext.Collections.Remove(collection);
        await _writeDbContext.SaveChangesAsync();
        await _fullTextSearch.Delete<CollectionFullTextSearchModel>(collection.Id);
    }

    public async Task RemoveCollectionItemAsync(RemoveCollectionItem command)
    {
        var collection = await GetAsync(command.CollectionId);

        if (collection is null)
        {
            throw new CollectionNotFoundException(command.CollectionId);
        }
        
        collection.RemoveItem(command.Name);

        await UpdateCollectionAsync(collection);
    }
}