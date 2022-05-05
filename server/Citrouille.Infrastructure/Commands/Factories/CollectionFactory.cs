using Citrouille.Data.Entities;

namespace Citrouille.Infrastructure.Commands.Factories;

public sealed class CollectionFactory : ICollectionFactory
{
    public Collection Create(Guid id, string name, string description, CollectionTheme theme, List<FieldTemplate> fields, List<Tag> tags)
        => new(id, name, description, theme, fields, tags);

    public Collection CreateWithItems(Guid id, string name, string description, CollectionTheme theme, List<FieldTemplate> fields, List<Tag> tags, List<CollectionItem> items)
    {
        var collection = Create(id, name, description, theme, fields, tags);
        collection.AddItems(items);
        return collection;
    }
}
