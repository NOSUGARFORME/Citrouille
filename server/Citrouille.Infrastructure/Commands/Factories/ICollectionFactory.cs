using Citrouille.Data.Entities;

namespace Citrouille.Infrastructure.Commands.Factories;

public interface ICollectionFactory
{
    Collection Create(Guid id, string title, string description, CollectionTheme theme, List<FieldTemplate> fields, List<Tag> tags);

    Collection CreateWithItems(Guid id, string name, string description, CollectionTheme theme, List<FieldTemplate> fields, List<Tag> tags, List<CollectionItem> items);
}