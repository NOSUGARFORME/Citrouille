using Citrouille.Domain.Entities;
using Citrouille.Domain.ValueObjects;

namespace Citrouille.Domain.Factories;

public interface ICollectionFactory
{
    Collection Create(CollectionId id, CollectionTitle name, CollectionDescription description, CollectionTheme theme,
        List<Tag> tags, List<FieldTemplate> fields);

    Collection CreateWithItems(CollectionId id, CollectionTitle name, CollectionDescription description, CollectionTheme theme,
        List<Tag> tags, List<FieldTemplate> fields, LinkedList<CollectionItem> items);
}