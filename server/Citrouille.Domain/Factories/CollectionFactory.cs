using Citrouille.Domain.Entities;
using Citrouille.Domain.ValueObjects;

namespace Citrouille.Domain.Factories;

public sealed class CollectionFactory : ICollectionFactory
{
    public Collection Create(CollectionId id, CollectionTitle name, CollectionDescription description,
        CollectionTheme theme,
        List<Tag> tags, List<FieldTemplate> fields)
        => new(id, name, description, theme, tags, fields);

    public Collection CreateWithItems(CollectionId id, CollectionTitle name, CollectionDescription description,
        CollectionTheme theme, List<Tag> tags, List<FieldTemplate> fields, LinkedList<CollectionItem> items)
    {
        var collection = Create(id, name, description, theme, tags, fields);
        collection.AddItems(items);
        return collection;
    }
}