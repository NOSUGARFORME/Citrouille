using Citrouille.Data.Entities;

namespace Citrouille.Infrastructure.Commands.Factories;

public sealed class CollectionFactory : ICollectionFactory
{
    public Collection Create(Guid id, string name, string description)
        => new(id, name, description);

    public Collection CreateWithItems(Guid id, string name, string description, List<CollectionItem> items)
    {
        var collection = Create(id, name, description);
        collection.AddItems(items);
        return collection;
    }
}
