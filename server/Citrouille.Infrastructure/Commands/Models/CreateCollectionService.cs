using Citrouille.Data.Entities;
using Citrouille.Infrastructure.Commands.Exceptions;
using Citrouille.Infrastructure.Commands.Factories;

namespace Citrouille.Infrastructure.Commands.Models;

public class CreateCollectionService
{
    private readonly ICollectionCommandService _repository;
    private readonly ICollectionFactory _factory;
    private readonly ICollectionReadService _readService;
    private readonly ICollectionThemeReadService _collectionThemeReadService;
    private readonly ICollectionTagReadService _collectionTagReadService;

    public CreateCollectionService(ICollectionReadService readService, ICollectionFactory factory, ICollectionCommandService repository, ICollectionThemeReadService collectionThemeReadService, ICollectionTagReadService collectionTagReadService)
    {
        _readService = readService;
        _factory = factory;
        _repository = repository;
        _collectionThemeReadService = collectionThemeReadService;
        _collectionTagReadService = collectionTagReadService;
    }

    public async Task CreateCollectionAsync(CreateCollection command)
    {
        var (id, title, description, tagsStrings, themeId, fieldTemplates) = command;
        
        if (await _readService.ExistsByTitleAsync(title))
        {
            throw new CollectionAlreadyExistsException(title);
        }

        var theme = await _collectionThemeReadService.GetByIdAsync(themeId);
        if (theme is null)
        {
            throw new MissingThemeException(themeId);
        }

        var tags = await GetTags(tagsStrings);
        var collection = _factory.Create(id, title, description, theme, fieldTemplates, tags.ToList());

        await _repository.CreateCollectionAsync(collection);
    }

    private async Task<List<Tag>> GetTags(List<string> tagNames)
    {
        var tags = new List<Tag>();
        foreach (var tagName in tagNames)
        {
            var tag = await GetTag(tagName);
            tags.Add(tag);
        }

        return tags;
    }
    
    private async Task<Tag> GetTag(string tagName)
    {
        var tag = await _collectionTagReadService.GetByNameAsync(tagName);
        return tag ?? new Tag(tagName);
    }
}