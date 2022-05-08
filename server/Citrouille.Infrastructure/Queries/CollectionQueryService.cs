using AutoMapper;
using Citrouille.Data;
using Citrouille.Data.Entities;
using Citrouille.Infrastructure.Queries.Models;
using Citrouille.Infrastructure.Queries.Models.Collections;
using Citrouille.Infrastructure.Services.FullTextSearch;
using Citrouille.Infrastructure.Services.FullTextSearch.Models;
using Microsoft.EntityFrameworkCore;

namespace Citrouille.Infrastructure.Queries;

public class CollectionQueryService
{
    private readonly DbSet<Collection> _collections;
    private readonly IFullTextSearchService _fullTextSearch;
    private readonly IMapper _mapper;
    
    public CollectionQueryService(CollectionDbContext context, IFullTextSearchService fullTextSearch, IMapper mapper)
    {
        _fullTextSearch = fullTextSearch;
        _mapper = mapper;
        _collections = context.Collections;
    }

    public Task<CollectionDto> GetById(Guid id)
    {
        return _collections
            .Include(c => c.Fields)
            .Include(c => c.Items)
            .ThenInclude(i => i.Fields)
            .Include(c => c.Tags)
            .ThenInclude(ct => ct.Tag)
            .Include(c => c.Theme)
            .Where(c => c.Id == id)
            .Select(c => c.AsDto())
            .AsNoTracking()
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<CollectionDto>> GetCollectionsByQuery(CollectionQuery query)
        => await _mapper
            .ProjectTo<CollectionDto>(await GetCollectionsQuery(query))
            .ToListAsync(); 
    
    private async Task<IQueryable<Collection>> GetCollectionsQuery(
        CollectionQuery query)
    {
        var dataQuery = _collections.AsQueryable();
        
        if (query.Theme.HasValue)
        {
            dataQuery = dataQuery.Where(c => c.Theme.Id == query.Theme);
        }

        if (!string.IsNullOrWhiteSpace(query.Title))
        {
            dataQuery = dataQuery.Where(c => c.Title.ToLower().Contains(query.Title.ToLower()));
        }

        if (string.IsNullOrWhiteSpace(query.Description)) return dataQuery;
        {
            var fullTextSearch = await _fullTextSearch
                .Query<CollectionFullTextSearchModel>(
                    c => c.Description,
                    query.Description);

            var ids = fullTextSearch.Select(c => c.Id);
            dataQuery = dataQuery.Where(c => ids.Contains(c.Id));
        }

        return dataQuery;
    }
}